using Game.Core;
using UnityEngine;

namespace Game.Gameplay.Player
{
    [RequireComponent(typeof(PlayerController))]
    /// <summary>
    /// Owns the player's state machine and interrupt handling.
    /// </summary>
    public class PlayerBehavior : MonoBehaviour
    {
        [SerializeField] private StateInterruptDefinition _interruptDefinition;
        protected PlayerController _controller;
        protected StateMachine<CharacterStateId> _stateMachine;
        protected StateInterruptor _interruptor;

        public void Initialize(PlayerController controller)
        {
            _controller = controller;
            _stateMachine = new StateMachine<CharacterStateId>();

            InitializeStates();
            InitializeInterruptor();

            _stateMachine.ChangeState(CharacterStateId.Idle);
        }

        private void InitializeStates()
        {
            _stateMachine.AddState(CharacterStateId.Idle, new IdleState(_stateMachine, _controller));
            _stateMachine.AddState(CharacterStateId.Move, new MoveState(_stateMachine, _controller));
            _stateMachine.AddState(CharacterStateId.Attack, new AttackState(_stateMachine, _controller));
            _stateMachine.AddState(CharacterStateId.Dead, new DeadState(_stateMachine, _controller));
        }

        private void InitializeInterruptor()
        {
            _interruptor = new StateInterruptor(_interruptDefinition, _stateMachine);
        }

        private void Update()
        {
            _stateMachine.Update();
        }
    }
}
