namespace Game.Gameplay
{
    public class ApplyStatusEffect : Effect
    {
        private readonly StatusEffectDefinition _definition;

        public ApplyStatusEffect(StatusEffectDefinition definition)
        {
            _definition = definition;
        }

        public override void Execute(EffectContext context)
        {
            context.Target.StatusEffectController.Add(_definition, context);
        }
    }
}
