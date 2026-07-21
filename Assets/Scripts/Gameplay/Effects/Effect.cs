using UnityEngine;

namespace Game.Gameplay
{
    /// <summary>
    /// Represents a runtime gameplay action.
    ///
    /// Effects do not maintain a lifecycle like StatusEffects.
    /// However, a runtime instance may need to be created before execution
    /// to capture values that must remain stable until the effect is applied.
    ///
    /// Example:
    /// A projectile deals 50 + 20% of the caster's ATK on hit.
    /// The damage is calculated when the projectile is spawned (effect creation),
    /// stored in the runtime effect, and applied when the projectile hits (effect execution).
    /// </summary>
    public abstract class Effect
    {
        public abstract void Execute(EffectContext context);
    }
}
