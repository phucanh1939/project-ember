namespace Game.Gameplay
{
    /// <summary>
    /// Deals damage to the target.
    /// </summary>
    public class DamageEffect : Effect
    {
        private readonly float _damage;

        public DamageEffect(float damage)
        {
            _damage = damage;
        }

        public override void Execute(EffectContext context)
        {
            context.Target.Health.TakeDamage(new DamageData(_damage));
        }
    }
}