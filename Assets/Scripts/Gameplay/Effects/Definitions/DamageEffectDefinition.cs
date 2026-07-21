using UnityEngine;

namespace Game.Gameplay
{
    [CreateAssetMenu(menuName = "Gameplay/Effects/Damage")]
    public class DamageEffectDefinition : EffectDefinition
    {
        [SerializeField] private DamageData _damage;

        public override void Execute(EffectContext context)
        {
            context.Target.Health.TakeDamage(_damage);
        }
    }
}