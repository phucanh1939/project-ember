using UnityEngine;

namespace Game.Gameplay
{
    [CreateAssetMenu(menuName = "Gameplay/Effects/Apply Status")]
    public class ApplyStatusEffectDefinition : EffectDefinition
    {
        [SerializeField] private StatusEffectDefinition _statusEffect;

        public override Effect CreateEffect(IEffectInstigator instigator)
        {
            return new ApplyStatusEffect(_statusEffect.CreateStatusEffect(instigator));
        }
    }
}