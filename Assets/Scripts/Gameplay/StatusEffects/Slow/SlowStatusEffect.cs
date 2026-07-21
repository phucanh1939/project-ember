namespace Game.Gameplay
{
    public class SlowStatusEffect : StatusEffect
    {
        private readonly float _slowPercent;
        private StatModifier _modifier;

        public SlowStatusEffect(float duration, float slowPercent) : base(duration)
        {
            _slowPercent = slowPercent;
        }

        protected override void OnApply(IEffectTarget target)
        {
            _modifier = new StatModifier(
                StatType.MoveSpeed,
                StatModifierType.Percent,
                StatModifierSource.StatusEffect,
                -_slowPercent);

            target.StatModifierContainer.AddModifier(_modifier);
        }

        protected override void OnExpire(IEffectTarget target)
        {
            target.StatModifierContainer.RemoveModifier(_modifier);
        }
    }
}