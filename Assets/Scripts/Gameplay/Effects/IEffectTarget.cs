namespace Game.Gameplay
{
    /// <summary>
    /// Represents an object that can receive gameplay effects.
    ///
    /// Exposes common gameplay systems required by effects.
    /// If this interface grows too large, split responsibilities into smaller interfaces.
    ///
    /// Example:
    /// IDamageable provides Health access.
    /// IHasStats provides stat access.
    /// IHasStatusEffects provides status effect access.
    /// </summary>
    public interface IEffectTarget : IEntity
    {
        public IDamageable Damageble { get; }
        public IStatModifierReceiver StatModifierReceiver { get; }
        public IStatusEffectReceiver StatusEffectReceiver { get; }
        public IStateInterruptor IStateInterruptor { get; }
    }
}
