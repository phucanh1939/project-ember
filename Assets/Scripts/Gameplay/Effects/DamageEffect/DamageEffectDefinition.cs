using UnityEngine;

namespace Game.Gameplay
{
    [CreateAssetMenu(menuName = "Gameplay/Effects/Damage")]
    public class DamageEffectDefinition : EffectDefinition
    {
        [SerializeField] private float _damage;

        public override Effect CreateEffect()
        {
            return new DamageEffect(_damage);
        }
    }
}