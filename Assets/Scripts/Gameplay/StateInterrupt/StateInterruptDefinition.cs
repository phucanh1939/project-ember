using System;
using UnityEngine;

namespace Game.Gameplay
{
    /// <summary>
    /// Defines priority rules for character interrupts.
    /// Higher priority interrupts override lower priority interrupts.
    /// </summary>
    [CreateAssetMenu(menuName = "Gameplay/State Interrupt Definition")]
    public class StateInterruptDefinition : ScriptableObject
    {
        [SerializeField] private StateInterruptConfig[] _interruptStates;
        [SerializeField] private CharacterStateId _defaultState;
    
        public CharacterStateId DefaultState => _defaultState;

        public StateInterruptConfig GetConfig(StateInterruptType type)
        {
            return _interruptStates[(int)type];
        }

        public CharacterStateId GetStateId(StateInterruptType type)
        {
            var config = _interruptStates[(int)type];
            return config.stateId;
        }

        public int GetPriority(StateInterruptType type)
        {
            var config = _interruptStates[(int)type];
            return config.priority;

        }
 
    }
}