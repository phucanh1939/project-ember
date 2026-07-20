namespace Game.Gameplay
{
    /// <summary>
    /// Represents a runtime modification applied to a character stat.
    ///
    /// Modifiers are provided by systems such as attributes, equipment, and status effects.
    /// </summary>
    public readonly struct StatModifier
    {
        public StatType StatType { get; }
        public StatModifierType ModifierType { get; }
        public StatModifierSource Source { get; }
        public float Value { get; }

        public StatModifier(StatType statType, StatModifierType modifierType, StatModifierSource source, float value)
        {
            StatType = statType;
            ModifierType = modifierType;
            Source = source;
            Value = value;
        }

        public float Apply(float currentValue)
        {
            return ModifierType switch
            {
                StatModifierType.Flat => currentValue + Value,
                StatModifierType.Percent => currentValue * (1 + Value / 100f),
                _ => currentValue
            };
        }
    }
}