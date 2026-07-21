using UnityEngine;

namespace Game.Gameplay
{
    [CreateAssetMenu(menuName = "Gameplay/Status Effects/Slow")]
    public class SlowStatusEffectDefinition : StatusEffectDefinition
    {
        [SerializeField] private float _slowPercent;

        public override StatusEffect CreateStatusEffect(IEffectInstigator instigator)
        {
            return new SlowStatusEffect(_duration, _slowPercent);
        }
    }
}