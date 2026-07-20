using UnityEngine;

namespace Game.Gameplay
{
    /// <summary>
    /// Defines a persistent status effect.
    /// </summary>
    public abstract class StatusEffectDefinition : ScriptableObject
    {
        public float Duration;

        public abstract StatusEffect CreateInstance(EffectContext context);
    }
}
