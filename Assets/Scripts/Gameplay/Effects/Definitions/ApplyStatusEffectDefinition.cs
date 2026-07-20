using UnityEngine;

namespace Game.Gameplay
{
    [CreateAssetMenu(menuName = "Gameplay/Effects/Apply Status")]
    public class ApplyStatusEffectDefinition : EffectDefinition
    {
        [SerializeField] private StatusEffectDefinition _status;

        public override void Execute(EffectContext context)
        {
            context.Target.StatusEffectController.Add(_status, context);
        }
    }
}