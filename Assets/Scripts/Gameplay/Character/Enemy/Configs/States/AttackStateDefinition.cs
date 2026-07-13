using Game.Core;
using UnityEngine;

namespace Game.Gameplay.Enemy
{
    [CreateAssetMenu(fileName = "Attack", menuName = "Game/Enemy/State/Attack")]
    public class AttackStateDefinition : StateDefinition
    {
        private void OnEnable()
        {
            SetId(StateId.Attack);
        }

        public override EnemyState Create(StateMachine<StateId> stateMachine, EnemyController controller)
        {
            return new AttackState(stateMachine, controller);
        }
    }
}