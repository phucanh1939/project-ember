using UnityEngine;

namespace Game.Gameplay
{
    [CreateAssetMenu(menuName = "Gameplay/Status Effects/Stun")]
    public class StunStatusEffectDefinition : StatusEffectDefinition
    {
        public override StatusEffect CreateStatusEffect(IEffectInstigator instigator)
        {
            return new StunStatusEffect(instigator.Faction, _targetMask, _duration);
        }
    }
}