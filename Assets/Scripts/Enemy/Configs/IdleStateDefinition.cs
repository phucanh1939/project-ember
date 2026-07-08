using UnityEngine;

namespace Game.Enemy
{
    [CreateAssetMenu(fileName = "Idle", menuName = "Game/Enemy/State/Idle")]
    public class IdleStateDefinition : StateDefinition
    {
        private void OnEnable()
        {
            SetId(StateId.Idle);
        }

        public override EnemyState Create(EnemyStateMachine stateMachine, EnemyController controller)
        {
            return new IdleState(stateMachine, controller);
        }
    }
}