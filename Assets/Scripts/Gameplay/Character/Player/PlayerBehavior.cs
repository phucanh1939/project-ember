using Game.Core;
using UnityEngine;

namespace Game.Gameplay.Player
{
    [RequireComponent(typeof(PlayerController))]
    public class PlayerBehavior : MonoBehaviour
    {
        protected PlayerController _controller;
        protected StateMachine<StateId> _stateMachine;
        protected CharacterStateInterruptor<StateId> _interruptor;

        public void Initialize(PlayerController controller)
        {
            _controller = controller;
            _stateMachine = new StateMachine<StateId>();

            InitializeStates();
            InitializeInterruptor();

            _stateMachine.ChangeState(StateId.Idle);
        }

        private void InitializeStates()
        {
            _stateMachine.AddState(StateId.Idle, new IdleState(_stateMachine, _controller));
            _stateMachine.AddState(StateId.Move, new MoveState(_stateMachine, _controller));
            _stateMachine.AddState(StateId.Attack, new AttackState(_stateMachine, _controller));
            _stateMachine.AddState(StateId.Dead, new DeadState(_stateMachine, _controller));
        }

        private void InitializeInterruptor()
        {
            var interruptStates = new CharacterInterruptStates<StateId>
            {
                Dead = StateId.Dead
            };

            _interruptor = new CharacterStateInterruptor<StateId>(
                _stateMachine,
                _controller.Health,
                _controller.StatusEffect,
                interruptStates
            );
        }

        private void Update()
        {
            _stateMachine.Update();
        }
    }
}