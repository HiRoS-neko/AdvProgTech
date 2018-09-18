using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;
using UnityEngine.Networking;

public class Insect : MonoBehaviour
{
    [SerializeField] private Rigidbody _rgd;
    [SerializeField, Range(0, 5)] private float _speed;

    [SerializeField, Range(0, 10)] private float _maxVelocity;

    // Use this for initialization
    void Start()
    {
        if (_rgd == null) _rgd = GetComponent<Rigidbody>();
    }

    public void Move(Vector3 dir, float rot)
    {
        //Add force to insect
        _rgd.AddForce(dir * _speed, ForceMode.VelocityChange);
        //Ensure velocity isn't greater than max velocity
        transform.Rotate(transform.up, rot);
    }

    public void Jump(float force)
    {
        _rgd.AddForce(transform.up * force, ForceMode.Impulse);
    }

    public void Attack(Vector3 point, Bullet.BulletType bulletType)
    {
        //todo fire bulletType at point
    }
}