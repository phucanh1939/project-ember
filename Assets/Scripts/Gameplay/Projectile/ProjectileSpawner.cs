using UnityEngine;

namespace Game.Gameplay
{
    /// <summary>
    /// Spawns projectiles for an effect instigator.
    /// </summary>
    public class ProjectileSpawner : MonoBehaviour, IProjectileSpawner
    {
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private Transform _projectileContainer;

        private IEffectInstigator _instigator;

        public void Initialize(IEffectInstigator instigator)
        {
            _instigator = instigator;
        }

        public void Spawn(ProjectileDefinition definition, Vector2 targetposition)
        {
            var direction = targetposition - (Vector2)_spawnPoint.position;
            definition.CreateProjectile(_instigator, _spawnPoint.position, direction, _projectileContainer);
        }
    }
}