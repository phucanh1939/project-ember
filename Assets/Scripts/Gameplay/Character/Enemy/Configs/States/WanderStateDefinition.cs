using UnityEngine;

namespace Game.Gameplay.Enemy
{
    [CreateAssetMenu(fileName = "Wander", menuName = "Game/Enemy/State/Wander")]
    public class WanderStateDefinition : StateDefinition
    {
        private void OnEnable()
        {
            SetId(StateId.Wander);
        }

        public override EnemyState Create(EnemyStateMachine stateMachine, EnemyController controller)
        {
            return new WanderState(stateMachine, controller);
        }
    }
}