
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
        private readonly StunStatusEffectDefinition _definition;
        private StateInterruptor _stateInterruptor;
        private int _interruptId = -1;

        public StunStatusEffect(StunStatusEffectDefinition definition, EffectContext context) : base(context)
        {
            _definition = definition;
            _stateInterruptor = context.Target.Behavior.StateInterruptor;
        }

        protected override void OnApply()
        {
            _interruptId = _stateInterruptor.AddInterrupt(StateInterruptType.Stun);
        }

        protected override void OnExpire()
        {
            if (_interruptId == -1)
                return;

            _stateInterruptor.RemoveInterrupt(_interruptId);
        }
    }
}
