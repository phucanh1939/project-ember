using Game.Core;
using UnityEngine;

namespace Game.Gameplay.Enemy
{
    public class EnemyBehavior : MonoBehaviour
    {
        [SerializeField] private EnemyAIConfig _config;

        protected EnemyController _controller;
        protected StateMachine<StateId> _stateMachine;
        protected CharacterStateInterruptor<StateId> _interruptor;

        public void Initialize(EnemyController controller)
        {
            _controller = controller;
            _stateMachine = new StateMachine<StateId>();
            InitializeStates();
            InitializeInterruptor();
            _stateMachine.ChangeState(_config.InitialState);
        }

        private void InitializeStates()
        {
            foreach (var definition in _config.States)
            {
                _stateMachine.AddState(definition.Id, definition.Create(_stateMachine, _controller));
            }
        }

        private void InitializeInterruptor()
        {
            var interruptStates = new CharacterInterruptStates<StateId>
            {
                Dead = StateId.Dead,
                // Stunned = StateId.Stunned,
                // Knockback = StateId.Knockback
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