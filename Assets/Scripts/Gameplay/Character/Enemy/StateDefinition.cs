using Game.Core;
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
        private CharacterStateId _id;

        public CharacterStateId Id => _id;

        protected void SetId(CharacterStateId id)
        {
            _id = id;
        }

        public abstract EnemyState Create(StateMachine<CharacterStateId> stateMachine, EnemyController controller);
    }
}