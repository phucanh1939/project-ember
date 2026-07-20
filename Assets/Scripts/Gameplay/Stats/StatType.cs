namespace Game.Gameplay
{
    /// <summary>
    /// Identifies a character stat that can receive modifiers.
    /// </summary>
    public enum StatType
    {
        MaxHealth,
        AttackDamage,
        Armor,
        AttackSpeed,
        MoveSpeed,

        Count, // This must be the last item, will be used as Number of Stat Type
    }
}
