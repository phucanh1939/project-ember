using UnityEngine;

namespace Game.Gameplay.Enemy
{
    [CreateAssetMenu(fileName = "Return", menuName = "Game/Enemy/State/Return")]
    public class ReturnStateDefinition : StateDefinition
    {
        private void OnEnable()
        {
            SetId(StateId.Return);
        }

        public override EnemyState Create(EnemyStateMachine stateMachine, EnemyController controller)
        {
            return new ReturnState(stateMachine, controller);
        }
    }
}