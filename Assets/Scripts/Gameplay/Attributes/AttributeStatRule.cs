using System;

namespace Game.Gameplay
{
    /// <summary>
    /// Defines the relationship between an attribute and a stat.
    /// </summary>
    [Serializable]
    public class AttributeStatRule
    {
        public AttributeType AttributeType;
        public StatType StatType;
        public float ValuePerPoint;
    }
}
