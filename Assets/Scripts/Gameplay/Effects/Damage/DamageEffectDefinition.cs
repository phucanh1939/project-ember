using UnityEngine;

namespace Game.Gameplay
{
    [CreateAssetMenu(menuName = "Gameplay/Effects/Damage")]
    public class DamageEffectDefinition : EffectDefinition
    {
        [SerializeField] private DamageType _damageType;
        [SerializeField] private float _baseAmount;
        [SerializeField] private float _statScalar;
        [SerializeField] private StatType _scalingStat;

        public override Effect CreateEffect(IEffectInstigator instigator)
        {
            var statValue = instigator.StatsProvider.GetStatValue(_scalingStat);
            var amount = _baseAmount + statValue * _statScalar;
            var damage = new DamageData(amount, _damageType);
            return new DamageEffect(instigator.Faction, _targetMask, damage);
        }
    }
}