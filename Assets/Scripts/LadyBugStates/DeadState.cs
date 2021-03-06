using UnityEngine;

namespace LadyBugStates
{
    public class DeadState : FSMState
    {
        private AIProperties _aiProperties;

        private LadyBugAi _ladyBugAi;

        public DeadState(AIProperties aiProperties, Transform npc)
        {
            stateID = FSMStateID.Dead;

            _ladyBugAi = npc.GetComponent<LadyBugAi>();
            _aiProperties = aiProperties;
            curSpeed = aiProperties.speed;
        }

        public override void Reason(Transform player, Transform npc)
        {
        }

        public override void Act(Transform player, Transform npc)
        {
        }

        public override void FixedAct(Transform player, Transform npc)
        {
        }
    }
}