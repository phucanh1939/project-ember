using UnityEngine;

namespace Game.Gameplay
{
    /// <summary>
    /// Contains the runtime information available when an effect is executed.
    ///
    /// The context describes the entity that caused the effect, the entity
    /// affected by it, and the world-space position relevant to the execution.
    /// </summary>
    public readonly struct EffectContext
    {
        /// <summary>
        /// The entity responsible for causing the effect.
        ///
        /// May be null when the effect has no specific instigator.
        /// </summary>
        public IEffectInstigator Instigator { get; }

        /// <summary>
        /// The entity directly affected by the effect.
        ///
        /// </summary>
        public IEffectTarget Target { get; }

        /// <summary>
        /// The world-space target position of the effect
        /// </summary>
        public Vector2 TargetPosition { get; }

        public EffectContext(
            IEffectInstigator instigator,
            IEffectTarget target,
            Vector2 targetPosition)
        {
            Instigator = instigator;
            Target = target;
            TargetPosition = targetPosition;
        }

        public EffectContext(
            IEffectInstigator instigator,
            IEffectTarget target)
            : this(instigator, target, Vector2.zero)
        {
        }

        public EffectContext(IEffectTarget target)
            : this(null, target, Vector2.zero)
        {
        }

        public EffectContext(IEffectTarget target, Vector2 targetPosition)
            : this(null, target, targetPosition)
        {
        }
    }
}
