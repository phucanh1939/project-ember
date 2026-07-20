using UnityEngine;

namespace Game.Gameplay.Enemy
{
    [CreateAssetMenu(fileName = "EnemyAIConfig", menuName = "Game/Enemy/AI Config")]
    /// <summary>
    /// Configures the initial state and available states for an enemy.
    /// </summary>
    public class EnemyAIConfig : ScriptableObject
    {
        [SerializeField] private StateId _initialState = StateId.Idle;
        [SerializeField] private StateDefinition[] _states;

        public StateId InitialState => _initialState;
        public StateDefinition[] States => _states;
    }
}
