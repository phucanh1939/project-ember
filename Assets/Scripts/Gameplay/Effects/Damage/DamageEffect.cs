namespace Game.Gameplay
{
    /// <summary>
    /// Applies resolved damage to a target.
    /// </summary>
    public class DamageEffect : Effect
    {
        private readonly DamageData _damage;

        public DamageEffect(EntityFaction faction, EntityRelation targetMask, DamageData damage)
            : base(faction, targetMask)
        {
            _damage = damage;
        }

        protected override void OnExecute(EffectContext context)
        {
            context.Target.Damageble.TakeDamage(_damage);
        }
    }
}