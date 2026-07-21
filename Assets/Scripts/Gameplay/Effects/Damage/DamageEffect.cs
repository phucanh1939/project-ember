namespace Game.Gameplay
{
    /// <summary>
    /// Applies resolved damage to a target.
    /// </summary>
    public class DamageEffect : Effect
    {
        private readonly DamageData _damage;

        public DamageEffect(DamageData damage)
        {
            _damage = damage;
        }

        public override void Execute(EffectContext context)
        {
            context.Target.Health.TakeDamage(_damage);
        }
    }
}
