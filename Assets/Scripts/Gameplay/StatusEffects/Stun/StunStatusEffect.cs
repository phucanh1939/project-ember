namespace Game.Gameplay
{
    /// <summary>
    /// Applies a stun interrupt to a target.
    ///
    /// The character cannot perform normal actions while the interrupt is active.
    /// The interrupt is removed when the effect expires.
    /// </summary>
    public class StunStatusEffect : StatusEffect
    {
        private int _interruptId = -1;

        public StunStatusEffect(EntityFaction faction, EntityRelation targetMask, float duration)
            : base(faction, targetMask, duration)
        {
        }

        protected override void OnApply(IEffectTarget target)
        {
            _interruptId = target.Behavior.StateInterruptor.AddInterrupt(StateInterruptType.Stun);
        }

        protected override void OnExpire(IEffectTarget target)
        {
            if (_interruptId == -1)
                return;

            target.Behavior.StateInterruptor.RemoveInterrupt(_interruptId);
        }
    }
}