namespace Game.Gameplay
{
    /// <summary>
    /// Represents the object responsible for causing an effect.
    /// </summary>
    public interface IEffectInstigator : IEntity
    {
        public CharacterStats Stats { get; }
        public ProjectileSpawner ProjectileSpawner { get; }
    }
}
