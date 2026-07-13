using System.Collections.Generic;
using System;
using UnityEngine;

namespace Game.Core
{
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
            Debug.Log("------- CHANGE STATE: " +id);
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