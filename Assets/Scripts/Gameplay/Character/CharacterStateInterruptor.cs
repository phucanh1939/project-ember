using System;
using Game.Core;

namespace Game.Gameplay
{
    public class CharacterStateInterruptor<TStateId>
    where TStateId : Enum
    {
        private readonly StateMachine<TStateId> _stateMachine;
        private readonly CharacterInterruptStates<TStateId> _states;

        public CharacterStateInterruptor(
            StateMachine<TStateId> stateMachine,
            Health health,
            StatusEffect status,
            CharacterInterruptStates<TStateId> states)
        {
            _stateMachine = stateMachine;
            _states = states;

            health.OnDeath += HandleDeath;
            status.OnStunned += HandleStun;
            status.OnKnockback += HandleKnockback;
        }


        private void HandleDeath()
        {
            _stateMachine.ChangeState(_states.Dead);
        }

        private void HandleStun()
        {
            _stateMachine.ChangeState(_states.Stunned);
        }

        private void HandleKnockback()
        {
            _stateMachine.ChangeState(_states.Knockback);
        }
    }
}
