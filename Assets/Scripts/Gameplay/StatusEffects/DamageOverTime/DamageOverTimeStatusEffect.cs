namespace Game.Gameplay
{
    /// <summary>
    /// Deals damage to the target at regular intervals.
    /// </summary>
    public class DamageOverTimeStatusEffect : StatusEffect
    {
        private readonly DamageData _damage;
        private readonly float _tickInterval;

        private float _tickTimer;

        public DamageOverTimeStatusEffect(EntityFaction faction, EntityRelation targetMask, float duration, float tickInterval, DamageData damage)
            : base(faction, targetMask, duration)
        {
            _damage = damage;
            _tickInterval = tickInterval;
            _tickTimer = tickInterval;
        }

        protected override void OnUpdate(float deltaTime, IEffectTarget target)
        {
            _tickTimer -= deltaTime;

            if (_tickTimer > 0)
                return;

            _tickTimer += _tickInterval;

            target.Health.TakeDamage(_damage);
        }
    }
}