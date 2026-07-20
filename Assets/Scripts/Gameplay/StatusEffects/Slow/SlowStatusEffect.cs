namespace Game.Gameplay
{
    public class SlowStatusEffect : StatusEffect
{
    private readonly SlowStatusEffectDefinition _definition;
    private StatModifier _modifier;

    public SlowStatusEffect(SlowStatusEffectDefinition definition, EffectContext context)
        : base(context)
    {
        _definition = definition;
    }

    protected override void OnApply()
    {
        _modifier = new StatModifier(
            StatType.MoveSpeed,
            StatModifierType.Percent,
            StatModifierSource.StatusEffect,
            -_definition.SlowPercent);

        _context.Target.StatModifierContainer.AddModifier(_modifier);
    }

    protected override void OnExpire()
    {
        _context.Target.StatModifierContainer.RemoveModifier(_modifier);
    }
}
}
