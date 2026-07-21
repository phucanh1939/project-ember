namespace Game.Gameplay
{
    /// <summary>
    /// Contains information about a damage event.
    /// </summary>
    public readonly struct DamageData
    {
        public readonly float damage;

        public DamageData(float damage)
        {
            this.damage = damage;
        }
    }
}