using System;

namespace Game.Gameplay
{
    /// <summary>
    /// Describes the relationship between two entities.
    /// </summary>
    [Flags]
    public enum EntityRelation
    {
        None    = 0,
        Self    = 1 << 0,
        Ally    = 1 << 1,
        Enemy   = 1 << 2,
        Neutral = 1 << 3
    }
}