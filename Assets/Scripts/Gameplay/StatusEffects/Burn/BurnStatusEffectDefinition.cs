using UnityEngine;

namespace Game.Gameplay
{
    [CreateAssetMenu(menuName = "Gameplay/Status Effects/Burn")]
    public class BurnStatusEffectDefinition : StatusEffectDefinition
    {
        [SerializeField] private float _damagePerTick;
        [SerializeField] private float _tickInterval;

        public float DamagePerTick => _damagePerTick;
        public float TickInterval => _tickInterval;

        public override StatusEffect CreateInstance(EffectContext context)
        {
            return new BurnStatusEffect(this, context);
        }
    }
}
