﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.UIElements;
using UnityEngine;
using LadyBugStates;
using UnityEngine.Experimental.PlayerLoop;
using UnityEngine.Serialization;

[System.Serializable]
public class AIProperties
{
    public float speed = 3;
    public float rotSpeed = 2;
    public float chaseDistance = 20;
    public float range = 5;
}


public class LadyBugAi : AdvancedFSM
{
    [SerializeField] public Insect LadyBug;

    private float health = 100;

    public float Health
    {
        get => health;
        set => health = Mathf.Clamp(health - value, 0, 100);
    }

    private string GetStateString()
    {
        string state;

        switch (CurrentStateID)
        {
            case FSMStateID.None:
                state = "NONE";
                break;
            case FSMStateID.Tracking:
                state = "TRACKING";
                break;
            case FSMStateID.Fleeing:
                state = "FLEEING";
                break;
            case FSMStateID.Dead:
                state = "DEAD";
                break;
            case FSMStateID.Idle:
                state = "IDLE";
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        return state;
    }

    protected override void Initialize()
    {
        GameObject objPlayer = GameObject.FindGameObjectWithTag("Player");
        playerTransform = objPlayer.transform;
        health = 100;
        ConstructFSM();
    }

    protected override void FSMUpdate()
    {
        elapsedTime += Time.deltaTime;

        if (CurrentState != null)
        {
            CurrentState.Reason(playerTransform, transform);
            CurrentState.Act(playerTransform, transform);
        }
    }

    protected override void FSMFixedUpdate()
    {
        if (CurrentState != null)
            CurrentState.FixedAct(playerTransform, transform);
    }

    private void ConstructFSM()
    {
        IdleState idleState = new IdleState(new AIProperties(), transform);
        idleState.AddTransition(Transition.SawPlayer, FSMStateID.Tracking);
        idleState.AddTransition(Transition.LowHealth, FSMStateID.Fleeing);
        idleState.AddTransition(Transition.NoHealth, FSMStateID.Dead);

        AddFSMState(idleState);

        TrackState trackState = new TrackState(new AIProperties {chaseDistance = 10}, transform);
        trackState.AddTransition(Transition.NoHealth, FSMStateID.Dead);
        trackState.AddTransition(Transition.LowHealth, FSMStateID.Fleeing);
        trackState.AddTransition(Transition.LostPlayer, FSMStateID.Idle);

        AddFSMState(trackState);

        DeadState deadState = new DeadState(new AIProperties {chaseDistance = 10}, transform);

        AddFSMState(deadState);

        FleeState fleeState = new FleeState(new AIProperties(), transform);
        fleeState.AddTransition(Transition.LostPlayer, FSMStateID.Idle);
        fleeState.AddTransition(Transition.NoHealth, FSMStateID.Dead);

        AddFSMState(fleeState);
    }
}