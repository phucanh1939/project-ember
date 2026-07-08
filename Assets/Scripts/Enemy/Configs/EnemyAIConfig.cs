using UnityEngine;

namespace Game.Enemy
{
    [CreateAssetMenu(fileName = "EnemyAIConfig", menuName = "Game/Enemy/AI Config")]
    public class EnemyAIConfig : ScriptableObject
    {
        [SerializeField] private StateId _initialState = StateId.Idle;
        [SerializeField] private StateDefinition[] _states;

        public StateId InitialState => _initialState;
        public StateDefinition[] States => _states;
    }
}