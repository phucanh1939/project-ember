using UnityEngine;

namespace Game.Gameplay.Enemy
{
    [CreateAssetMenu(fileName = "Chase", menuName = "Game/Enemy/State/Chase")]
    public class ChaseStateDefinition : StateDefinition
    {
        private void OnEnable()
        {
            SetId(StateId.Chase);
        }

        public override EnemyState Create(EnemyStateMachine stateMachine, EnemyController controller)
        {
            return new ChaseState(stateMachine, controller);
        }
    }
}