using UnityEngine;

namespace Game.Gameplay
{
    /// <summary>
    /// Defines an effect that spawns a projectile.
    /// </summary>
    [CreateAssetMenu(menuName = "Gameplay/Effects/Spawn Projectile")]
    public class SpawnProjectileEffectDefinition : EffectDefinition
    {
        [SerializeField] private ProjectileDefinition _projectile;

        public override Effect CreateEffect(IEffectInstigator instigator)
        {
            return new SpawnProjectileEffect(instigator.Faction, _targetMask, _projectile);
        }
    }
}