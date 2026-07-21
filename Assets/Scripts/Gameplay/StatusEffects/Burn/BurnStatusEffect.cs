namespace Game.Gameplay
{
    public class BurnStatusEffect : StatusEffect
    {
        private readonly BurnStatusEffectDefinition _definition = null;
        private float _tickTimer = 0f;

        public BurnStatusEffect(BurnStatusEffectDefinition definition, EffectContext context)
            : base(context)
        {
            _definition = definition;
        }

        protected override void OnUpdate(float deltaTime)
        {
            _tickTimer -= deltaTime;

            if (_tickTimer > 0)
                return;

            _tickTimer = _definition.TickInterval;

            _context.Target.Health.TakeDamage(new DamageData(_definition.DamagePerTick));
        }
    }
}