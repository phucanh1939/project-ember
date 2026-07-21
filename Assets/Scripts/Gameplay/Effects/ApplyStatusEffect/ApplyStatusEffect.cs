namespace Game.Gameplay
{
    public class ApplyStatusEffect : Effect
    {
        private readonly StatusEffect _statusEffect;

        public ApplyStatusEffect(StatusEffect statusEffect)
        {
            _statusEffect = statusEffect;
        }

        public override void Execute(EffectContext context)
        {
            context.Target.StatusEffectController.Add(_statusEffect);
        }
    }
}