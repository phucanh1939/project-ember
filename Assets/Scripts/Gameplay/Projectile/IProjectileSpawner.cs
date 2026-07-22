using UnityEngine;

namespace Game.Gameplay
{
    /// <summary>
    /// Spawns projectiles for an effect instigator.
    /// </summary>
    public interface IProjectileSpawner 
    {
        void Spawn(ProjectileDefinition definition, Vector2 targetPosition);
    }
}