using UnityEngine;

namespace LadyBugStates
{
    public class TrackState : FSMState
    {
        private AIProperties _aiProperties;

        private LadyBugAi _ladyBugAi;
        private bool _shootDelay = false;

        public TrackState(AIProperties aiProperties, Transform npc)
        {
            stateID = FSMStateID.Tracking;
            _ladyBugAi = npc.GetComponent<LadyBugAi>();
            _aiProperties = aiProperties;
        }

        public override void Reason(Transform player, Transform npc)
        {
            destPos = player.position;

            if (_ladyBugAi.Health <= 0)
            {
                _ladyBugAi.PerformTransition(Transition.NoHealth);
            }
            else if (_ladyBugAi.Health < 25)
            {
                _ladyBugAi.PerformTransition(Transition.LowHealth);
            }
            else if (Vector3.Distance(player.position, npc.position) > _aiProperties.chaseDistance)
            {
                _ladyBugAi.PerformTransition(Transition.LostPlayer);
            }
        }

        public override void Act(Transform player, Transform npc)
        {
            if (IsInCurrentRange(npc.transform, player.transform.position, _aiProperties.range) &&
                Random.Range(0, 100) == 0)
            {
                _ladyBugAi.LadyBug.Attack(player.transform.position, Bullet.BulletType.WebBullet, LayerMask.NameToLayer("EnemyBullet"));
                _shootDelay = true;
            }
        }

        public override void FixedAct(Transform player, Transform npc)
        {
            destPos = player.position;
            Vector3 newPos = Vector3.MoveTowards(npc.position, destPos, Time.fixedDeltaTime * _aiProperties.speed);
            _ladyBugAi.LadyBug.Move(npc.InverseTransformPoint(newPos).normalized);
        }
    }
}