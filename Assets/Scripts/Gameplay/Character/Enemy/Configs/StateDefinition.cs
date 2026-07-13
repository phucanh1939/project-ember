using UnityEngine;

namespace Game.Gameplay.Enemy
{
    /// <summary>
    /// Defines how to create a runtime enemy state.
    ///
    /// This is a ScriptableObject asset used for AI configuration.
    /// It does not contain runtime behavior.
    ///
    /// Example:
    /// ChaseStateDefinition
    ///     creates
    /// ChaseState
    /// </summary>
    public abstract class StateDefinition : ScriptableObject
    {
        [SerializeField]
        private StateId _id;

        public StateId Id => _id;

        protected void SetId(StateId id)
        {
            _id = id;
        }

        public abstract EnemyState Create(EnemyStateMachine stateMachine, EnemyController controller);
    }
}