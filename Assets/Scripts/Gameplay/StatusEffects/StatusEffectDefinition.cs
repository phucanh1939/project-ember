using UnityEngine;

namespace Game.Gameplay
{
    /// <summary>
    /// Defines a persistent status effect.
    /// </summary>
    public abstract class StatusEffectDefinition : ScriptableObject
    {
        [SerializeField] protected EntityRelation _targetMask;
        [SerializeField] protected float _duration;

        public abstract StatusEffect CreateStatusEffect(IEffectInstigator instigator);
    }
}
