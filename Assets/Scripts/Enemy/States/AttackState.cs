using UnityEngine;

namespace Game.Enemy
{
    /// <summary>
    /// Enemy combat state.
    ///
    /// Behavior:
    /// - Perform attacks against the current target.
    /// - Control attack timing and cooldown.
    ///
    /// Transitions:
    /// - Attack -> Chase: Target leaves attack range.
    /// - Attack -> Return: Target lost or exceeds leash range.
    /// </summary>
    public class AttackState : EnemyState
    {

        public AttackState(
            EnemyStateMachine stateMachine,
            EnemyController controller)
            : base(stateMachine, controller)
        {
        }


        public override void Enter()
        {
        }


        public override void Update()
        {
        }


        public override void Exit()
        {
        }
    }
}