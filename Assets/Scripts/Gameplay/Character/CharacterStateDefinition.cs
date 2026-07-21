using Game.Core;
using UnityEngine;

namespace Game.Gameplay
{
    /// <summary>
    /// Defines how to create a runtime enemy state.
    ///
    /// This is a ScriptableObject asset used for state configuration.
    /// It does not contain runtime behavior.
    ///
    /// Example:
    /// ChaseStateDefinition
    ///     creates
    /// ChaseState
    /// </summary>
    public abstract class CharacterStateDefinition : ScriptableObject
    {
        [SerializeField]
        private CharacterStateId _id;

        public CharacterStateId Id => _id;

        protected void SetId(CharacterStateId id)
        {
            _id = id;
        }

        public abstract CharacterState Create(StateMachine<CharacterStateId> stateMachine, Character controller);
    }
}