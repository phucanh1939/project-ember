using UnityEngine;

namespace Game.Gameplay
{
    /// <summary>
    /// Configures the initial state and available states for an enemy.
    /// </summary>
    [CreateAssetMenu(fileName = "CharacterBehaviorDefinition", menuName = "Gameplay/Character/Behavior Definition")]
    public class CharacterBehaviorDefinition : ScriptableObject
    {
        [SerializeField] private CharacterStateId _initialState = CharacterStateId.Idle;
        [SerializeField] private CharacterStateDefinition[] _states;

        public CharacterStateId InitialState => _initialState;
        public CharacterStateDefinition[] States => _states;
    }
}
