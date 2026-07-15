namespace Game.Gameplay
{
    /// <summary>
    /// Defines how a stat modifier changes its target value.
    /// </summary>
    public enum ModifierType
    {
        Flat,
        Percent
    }

    /// <summary>
    /// Represents one runtime change to a character stat.
    /// </summary>
    public class StatModifier
    {
        public StatType Type { get; }
        public ModifierType ModifierType { get; }
        public float Value { get; }

        public StatModifier(StatType type, float value, ModifierType modifierType)
        {
            Type = type;
            Value = value;
            ModifierType = modifierType;
        }
    }
}
