using UnityEngine;

namespace Game.Gameplay
{
    /// <summary>
    /// Defines a persistent status effect.
    /// </summary>
    public abstract class StatusEffectDefinition : ScriptableObject
    {
        [SerializeField] protected float _duration;

        public float Duration => _duration;

        public abstract StatusEffect CreateStatusEffect(IEffectInstigator instigator);
    }
}
