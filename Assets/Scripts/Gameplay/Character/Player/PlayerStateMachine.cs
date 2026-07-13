using System.Collections.Generic;
using UnityEngine;

namespace Game.Gameplay.Player
{
    /// <summary>
    /// Executes player states.
    ///
    /// Responsibilities:
    /// - Store available states.
    /// - Switch active state.
    /// - Manage state lifecycle.
    ///
    /// This class does NOT:
    /// - Create gameplay decisions.
    /// - Handle player behavior.
    ///
    /// Player states define behavior.
    /// </summary>
    public class PlayerStateMachine
    {
        private readonly Dictionary<StateId, PlayerState> _states = new();

        private PlayerState _currentState;

        public PlayerState CurrentState => _currentState;

        public PlayerStateMachine(PlayerController controller)
        {
            AddState(StateId.Idle, new IdleState(this, controller));
            AddState(StateId.Move, new MoveState(this, controller));
            AddState(StateId.Attack, new AttackState(this, controller));
            ChangeState(StateId.Idle);
        }

        public void AddState(StateId id, PlayerState state)
        {
            _states.Add(id, state);
        }

        public void ChangeState(StateId id)
        {
            if (!_states.TryGetValue(id, out PlayerState nextState))
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