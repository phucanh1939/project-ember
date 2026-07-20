using System;
using System.Collections.Generic;
using Game.Core;

namespace Game.Gameplay
{
    /// <summary>
    /// Controls forced character state changes caused by gameplay effects.
    ///
    /// Interrupt states override normal behavior while active interrupts exist.
    /// The highest priority active interrupt determines the current state.
    /// </summary>
    public class StateInterruptor
    {
        private readonly StateInterruptDefinition _definition;
        private readonly StateMachine<CharacterStateId> _stateMachine;

        // Track interrupt instances by ID because multiple sources can apply the same interrupt type.
        // Example: Stun from Ability A and Stun from Ability B are two separate interrupts.
        // Removing Ability B's stun should not remove Ability A's stun.
        private readonly Dictionary<int, StateInterruptType> _interrupts = new();

        private int _nextId = 0;

        public StateInterruptor(StateInterruptDefinition definition, StateMachine<CharacterStateId> stateMachine)
        {
            _definition = definition;
            _stateMachine = stateMachine;
        }

        public int AddInterrupt(StateInterruptType type)
        {
            int id = _nextId++;
            _interrupts.Add(id, type);
            UpdateState();
            return id;
        }

        public void RemoveInterrupt(int id)
        {
            if (!_interrupts.Remove(id))
                return;
            UpdateState();
        }

        private void UpdateState()
        {
            if (_interrupts.Count == 0)
            {
                _stateMachine.ChangeState(_definition.DefaultState);
                return;
            }
            _stateMachine.ChangeState(_definition.GetStateId(GetHighestPriorityInterrupt()));
        }

        private StateInterruptType GetHighestPriorityInterrupt()
        {
            StateInterruptType highest = StateInterruptType.Stun;
            int highestPriority = 0;

            foreach (var interrupt in _interrupts.Values)
            {
                int priority = _definition.GetPriority(interrupt);

                if (priority <= highestPriority)
                    continue;

                highest = interrupt;
                highestPriority = priority;
            }

            return highest;
        }
    }
}