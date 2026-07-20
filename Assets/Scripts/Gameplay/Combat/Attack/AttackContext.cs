namespace Game.Gameplay
{
    /// <summary>
    /// Provides an attack definition with its runtime attacker.
    /// </summary>
    public class AttackContext
    {
        public IAttacker Attacker { get; }

        public AttackContext(IAttacker attacker)
        {
            Attacker = attacker;
        }
    }
}
