namespace Game.Gameplay
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
    public enum CharacterStateId
    {
        /// <summary>
        /// No active behavior.
        /// Enemy is waiting or inactive.
        /// </summary>
        Idle,

        /// <summary>
        /// Moving
        /// </summary>
        Move,

        /// <summary>
        /// Performing an attack behavior.
        /// </summary>
        Attack,

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
        /// Returning to original position after losing target.
        /// </summary>
        Return,

        /// <summary>
        /// Enemy is dead and no longer performs actions.
        /// </summary>
        Dead
    }
}