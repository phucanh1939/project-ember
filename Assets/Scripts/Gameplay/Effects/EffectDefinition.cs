using UnityEngine;

namespace Game.Gameplay
{
    /// <summary>
    /// Defines a gameplay effect configuration.
    /// Creates runtime effects that can be executed.
    /// </summary>
    public abstract class EffectDefinition : ScriptableObject
    {
        public abstract Effect CreateEffect();
    }
}
