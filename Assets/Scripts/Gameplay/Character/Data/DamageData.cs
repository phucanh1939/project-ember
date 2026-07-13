namespace Game.Gameplay
{
    /// <summary>
    /// Contains information about a damage event.
    /// </summary>
    public struct DamageData
    {
        public int Amount;

        public DamageData(int amount)
        {
            Amount = amount;
        }
    }
}
