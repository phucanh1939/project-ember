using UnityEngine;

namespace Game.Gameplay
{
    /// <summary>
    /// Contains temporary runtime information required to execute an effect.
    ///
    /// The context describes the source of the effect, its target, and the
    /// world-space origin and direction used to execute it.
    /// </summary>
    public readonly struct EffectContext
    {
        /// <summary>
        /// The entity that caused or owns the effect.
        /// </summary>
        public IEffectInstigator Instigator { get; }

        /// <summary>
        /// The entity affected by the effect.
        /// May be null for effects that do not have a specific target.
        /// </summary>
        public IEffectTarget Target { get; }

        /// <summary>
        /// The world-space position where the effect originates.
        /// Used by AOE (both Point-targeted and Directional), to query targets
        /// 
        /// </summary>
        public Vector2 OriginPosition { get; }

        /// <summary>
        /// The normalized direction in which the effect is executed.
        /// Used by Directional AOE, to query target
        ///
        /// A zero vector represents an effect with no specific direction.
        /// </summary>
        public Vector2 Direction { get; }

        public EffectContext(
            IEffectInstigator instigator,
            IEffectTarget target,
            Vector2 originPosition,
            Vector2 direction)
        {
            Instigator = instigator;
            Target = target;
            OriginPosition = originPosition;
            Direction = direction;
        }
    }
}