namespace Game.Gameplay
{
    public class AttackContext
    {
        public IAttacker Attacker { get; }

        public AttackContext(IAttacker attacker)
        {
            Attacker = attacker;
        }
    }
}