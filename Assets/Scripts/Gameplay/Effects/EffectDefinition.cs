using UnityEngine;

namespace Game.Gameplay
{
    /// <summary>
    /// Defines an instant gameplay action.
    ///
    /// Effects are applied immediately when executed and do not maintain runtime state.
    /// Unlike StatusEffects, they do not require a runtime instance or lifecycle management.
    /// 
    /// The code where the effect executed live in the effect source (e.g Ability Execute, Projectile hit)
    /// Example: Ability trigger -> apply effects imidiately
    /// </summary>
    public abstract class EffectDefinition : ScriptableObject
    {
        public abstract void Execute(EffectContext context);
    }
}