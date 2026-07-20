using UnityEngine;

namespace Game.Gameplay.Enemy
{
    [CreateAssetMenu(fileName = "EnemyBehaviorDefinition", menuName = "Gameplay/Character/Enemy/Behavior Definition")]
    /// <summary>
    /// Configures the initial state and available states for an enemy.
    /// </summary>
    public class EnemyBehaviorDefinition : ScriptableObject
    {
        [SerializeField] private CharacterStateId _initialState = CharacterStateId.Idle;
        [SerializeField] private StateDefinition[] _states;

        public CharacterStateId InitialState => _initialState;
        public StateDefinition[] States => _states;
    }
}
