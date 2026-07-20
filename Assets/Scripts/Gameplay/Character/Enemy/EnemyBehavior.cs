using Game.Core;
using UnityEngine;

namespace Game.Gameplay.Enemy
{
    /// <summary>
    /// Owns the configured state machine and interrupts for an enemy.
    /// </summary>
    public class EnemyBehavior : MonoBehaviour
    {
        [SerializeField] private EnemyBehaviorDefinition _behaviorDefinition;
        [SerializeField] private StateInterruptDefinition _interruptDefinition;

        protected EnemyController _controller;
        protected StateMachine<CharacterStateId> _stateMachine;
        protected StateInterruptor _interruptor;

        public void Initialize(EnemyController controller)
        {
            _controller = controller;
            _stateMachine = new StateMachine<CharacterStateId>();
            InitializeStates();
            InitializeInterruptor();
            _stateMachine.ChangeState(_behaviorDefinition.InitialState);
        }

        private void InitializeStates()
        {
            foreach (var definition in _behaviorDefinition.States)
            {
                _stateMachine.AddState(definition.Id, definition.Create(_stateMachine, _controller));
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
