using UnityEngine;

namespace Game.Gameplay
{
    [CreateAssetMenu(menuName = "Gameplay/Status Effects/Slow")]
    public class SlowStatusEffectDefinition : StatusEffectDefinition
    {
        [SerializeField] private float _slowPercent;

        public override StatusEffect CreateStatusEffect(IEffectInstigator instigator)
        {
            return new SlowStatusEffect(instigator.Faction, _targetMask, _duration, _slowPercent);
        }
    }
}