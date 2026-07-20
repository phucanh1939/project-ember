using UnityEngine;

namespace Game.Gameplay
{
    [CreateAssetMenu(menuName = "Gameplay/Effects/Apply Status")]
    public class ApplyStatusEffectDefinition : EffectDefinition
    {
        [SerializeField] private StatusEffectDefinition _status;

        public override Effect CreateEffect()
        {
            return new ApplyStatusEffect(_status);
        }
    }
}