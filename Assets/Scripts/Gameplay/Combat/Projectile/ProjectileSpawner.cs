using UnityEngine;

namespace Game.Gameplay
{
    /// <summary>
    /// Spawns projectiles for an effect instigator.
    /// </summary>
    public class ProjectileSpawner : MonoBehaviour
    {
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private Transform _projectileContainer;

        private IEffectInstigator _instigator;

        public void Initialize(IEffectInstigator instigator)
        {
            _instigator = instigator;
        }

        public Projectile Spawn(ProjectileDefinition definition, Vector2 aimPosition)
        {
            var direction = aimPosition - (Vector2)_spawnPoint.position;
            return definition.CreateProjectile(_instigator, _spawnPoint.position, direction, _projectileContainer);
        }
    }
}