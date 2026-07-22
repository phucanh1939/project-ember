using UnityEngine;

namespace Game.Gameplay
{
    /// <summary>
    /// Represents the object responsible for causing an effect.
    /// </summary>
    public interface IEffectInstigator : IEntity
    {
        public IStatProvider StatsProvider { get; }
        public IProjectileSpawner ProjectileSpawner { get; }
        public Vector2 Position { get; }
    }
}
