namespace Game.Gameplay
{
    /// <summary>
    /// Provides movement speed for movable objects.
    /// </summary>
    public interface IMovementSpeedProvider
    {
        float MoveSpeed { get; }
    }
}