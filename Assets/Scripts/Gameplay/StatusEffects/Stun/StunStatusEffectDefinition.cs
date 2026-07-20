using UnityEngine;

namespace Game.Gameplay
{
    [CreateAssetMenu(menuName = "Gameplay/Status Effects/Stun")]
    public class StunStatusEffectDefinition : StatusEffectDefinition
    {
        public override StatusEffect CreateInstance(EffectContext context)
        {
            return new StunStatusEffect(this, context);
        }
    }
}
