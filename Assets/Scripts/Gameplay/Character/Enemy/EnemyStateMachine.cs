using System.Collections.Generic;
using UnityEngine;

namespace Game.Gameplay.Enemy
{
    /// <summary>
    /// Executes enemy states.
    ///
    /// Responsibilities:
    /// - Store available states.
    /// - Switch active state.
    /// - Manage state lifecycle.
    ///
    /// This class does NOT:
    /// - Create states.
    /// - Decide enemy behavior.
    /// - Know about specific enemy types.
    ///
    /// EnemyBrain provides the state configuration.
    /// </summary>
    public class EnemyStateMachine
    {
        private readonly Dictionary<StateId, EnemyState> _states = new();
        private EnemyState _currentState;

        public EnemyState CurrentState => _currentState;

        public void AddState(StateId id, EnemyState state)
        {
            _states.Add(id, state);
        }

        public void ChangeState(StateId id)
        {
            if (!_states.TryGetValue(id, out EnemyState nextState))
            {
                Debug.LogError($"State {id} is not registered.");
                return;
            }

            if (_currentState == nextState)
                return;

            _currentState?.Exit();
            _currentState = nextState;
            _currentState.Enter();
        }

        public void Update()
        {
            _currentState?.Update();
        }

        public bool HasState(StateId id)
        {
            return _states.ContainsKey(id);
        }
    }
}