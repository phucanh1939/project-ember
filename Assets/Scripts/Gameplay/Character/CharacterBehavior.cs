using Game.Core;
using UnityEngine;

namespace Game.Gameplay
{
    /// <summary>
    /// Owns the configured state machine and interrupts for an enemy.
    /// </summary>
    public class CharacterBehavior : MonoBehaviour
    {
        [SerializeField] private CharacterBehaviorDefinition _behaviorDefinition;
        [SerializeField] private StateInterruptDefinition _interruptDefinition;

        protected StateMachine<CharacterStateId> _stateMachine;
        protected StateInterruptor _interruptor;

        public StateInterruptor StateInterruptor => _interruptor;

        public void Initialize(CharacterController controller)
        {
            _stateMachine = new StateMachine<CharacterStateId>();
            InitializeStates(controller);
            InitializeInterruptor();
            _stateMachine.ChangeState(_behaviorDefinition.InitialState);
        }

        private void InitializeStates(CharacterController controller)
        {
            foreach (var definition in _behaviorDefinition.States)
            {
                _stateMachine.AddState(definition.Id, definition.Create(_stateMachine, controller));
            }
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
