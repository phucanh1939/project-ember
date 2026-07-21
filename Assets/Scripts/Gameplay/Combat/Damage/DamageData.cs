namespace Game.Gameplay
{
    /// <summary>
    /// Contains information about a damage event.
    /// </summary>
    public readonly struct DamageData
    {
        public float Amount { get; }
        public DamageType Type { get; }

        public DamageData(float amount, DamageType type)
        {
            Amount = amount;
            Type = type;
        }
    }
}