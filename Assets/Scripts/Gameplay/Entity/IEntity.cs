namespace Game.Gameplay
{
    /// <summary>
    /// Represents a gameplay entity that can participate in relationships
    /// with other entities.
    /// </summary>
    public interface IEntity
    {
        EntityFaction Faction { get; }
    }
}
