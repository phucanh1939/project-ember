namespace Game.Player
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
        /// </summary>
        Idle,

        /// <summary>
        /// Moving.
        /// </summary>
        Move,

        /// <summary>
        /// Performing an attack behavior.
        /// </summary>
        Attack,

        /// <summary>
        /// Enemy is dead and no longer performs actions.
        /// </summary>
        Dead
    }
}