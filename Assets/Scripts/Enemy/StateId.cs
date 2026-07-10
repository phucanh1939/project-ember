namespace Game.Enemy
{
    /// <summary>
    /// Identifies enemy behavior states.
    ///
    /// Used by EnemyStateMachine to locate registered states.
    /// The enum represents a behavior category, not a concrete class.
    ///
    /// Example:
    /// EnemyStateId.Attack can map to:
    /// - MeleeAttackState
    /// - RangedAttackState
    /// - BossAttackState
    /// depending on the enemy type.
    /// </summary>
    public enum StateId
    {
        /// <summary>
        /// No active behavior.
        /// Enemy is waiting or inactive.
        /// </summary>
        Idle,

        /// <summary>
        /// Moving around without a target.
        /// Example: random wandering or predefined route.
        /// </summary>
        Wander,

        /// <summary>
        /// Moving toward a detected target.
        /// </summary>
        Chase,

        /// <summary>
        /// Performing an attack behavior.
        /// </summary>
        Attack,

        /// <summary>
        /// Returning to original position after losing target.
        /// </summary>
        Return,

        /// <summary>
        /// Enemy is dead and no longer performs actions.
        /// </summary>
        Dead
    }
}