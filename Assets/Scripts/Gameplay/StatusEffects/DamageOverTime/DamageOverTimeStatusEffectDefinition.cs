using UnityEngine;

namespace Game.Gameplay
{
    [CreateAssetMenu(menuName = "Gameplay/Status Effects/Damage Over Time")]
    public class DamageOverTimeStatusEffectDefinition : StatusEffectDefinition
    {
        [SerializeField] private DamageType _damageType;
        [SerializeField] private float _baseDamage;
        [SerializeField] private StatType _scalingStat;
        [SerializeField] private float _damageScalar;
        [SerializeField] private float _tickInterval;

        public override StatusEffect CreateStatusEffect(IEffectInstigator instigator)
        {
            var elementalDamage = instigator.Stats.GetStatValue(_scalingStat);
            var damageAmount = _baseDamage + elementalDamage * _damageScalar;
            var damage = new DamageData(damageAmount, _damageType);
            return new DamageOverTimeStatusEffect(instigator.Faction, _targetMask, _duration, _tickInterval, damage);
        }
    }
}