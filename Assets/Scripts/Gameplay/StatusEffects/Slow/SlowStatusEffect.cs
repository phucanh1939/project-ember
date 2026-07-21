namespace Game.Gameplay
{
    public class SlowStatusEffect : StatusEffect
    {
        private readonly float _slowPercent;
        private StatModifier _modifier;

        public SlowStatusEffect(EntityFaction faction, EntityRelation targetMask, float duration, float slowPercent)
            : base(faction, targetMask, duration)
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