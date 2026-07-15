using System.Collections.Generic;
using System;
using UnityEngine;

namespace Game.Core
{
    /// <summary>
    /// Manages transitions and updates for a set of states.
    /// </summary>
    public class StateMachine<TStateId> where TStateId : Enum
    {
        private readonly Dictionary<TStateId, State<TStateId>> _states = new();

        private State<TStateId> _currentState;

        public void AddState(TStateId id, State<TStateId> state)
        {
            _states.Add(id, state);
        }

        public void ChangeState(TStateId id)
        {
            if (!_states.TryGetValue(id, out var next))
                return;

            _currentState?.Exit();
            _currentState = next;
            _currentState.Enter();
        }

        public void Update()
        {
            _currentState?.Update();
        }
    }
}
