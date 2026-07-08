using UnityEngine;

namespace Game.Enemy
{
    public class EnemyBrain : MonoBehaviour
    {
        [SerializeField]
        private EnemyAIConfig _config;

        protected EnemyController _controller;
        protected EnemyStateMachine _stateMachine;


        public void Initialize(EnemyController controller)
        {
            _controller = controller;
            _stateMachine = new EnemyStateMachine();
            InitializeStates();
            _stateMachine.ChangeState(_config.InitialState);
        }

        private void InitializeStates()
        {
            foreach (var definition in _config.States)
            {
                _stateMachine.AddState(definition.Id, definition.Create(_stateMachine, _controller));
            }
        }

        private void Update()
        {
            _stateMachine.Update();
        }
    }
}