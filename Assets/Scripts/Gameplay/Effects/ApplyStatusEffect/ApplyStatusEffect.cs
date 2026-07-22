namespace Game.Gameplay
{
    public class ApplyStatusEffect : Effect
    {
        private readonly StatusEffect _statusEffect;

        public ApplyStatusEffect(EntityFaction faction, EntityRelation targetMask, StatusEffect statusEffect)
            : base(faction, targetMask)
        {
            _statusEffect = statusEffect;
        }

        protected override void OnExecute(EffectContext context)
        {
            context.Target.StatusEffectController.Add(_statusEffect);
        }
    }
}