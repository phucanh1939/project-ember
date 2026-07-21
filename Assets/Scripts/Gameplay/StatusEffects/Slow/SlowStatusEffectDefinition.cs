using UnityEngine;

namespace Game.Gameplay
{
    [CreateAssetMenu(menuName = "Gameplay/Status Effects/Slow")]
    public class SlowStatusEffectDefinition : StatusEffectDefinition
    {
        [SerializeField] private float _slowPercent;

        public float SlowPercent => _slowPercent;

        public override StatusEffect CreateInstance(EffectContext context)
        {
            return new SlowStatusEffect(this, context);
        }
    }
}
