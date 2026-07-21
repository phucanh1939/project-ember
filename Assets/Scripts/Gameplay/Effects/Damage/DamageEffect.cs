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

        public override void Execute(IEffectTarget target)
        {
            target.Health.TakeDamage(_damage);
        }
    }
}
