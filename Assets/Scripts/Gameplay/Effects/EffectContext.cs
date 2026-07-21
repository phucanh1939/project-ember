namespace Game.Gameplay
{
    /// <summary>
    /// Contains runtime information required when executing an effect.
    /// </summary>
    public readonly struct EffectContext
    {
        public IEffectInstigator Instigator { get; }
        public IEffectTarget Target { get; }

        public EffectContext(IEffectInstigator instigator, IEffectTarget target)
        {
            Instigator = instigator;
            Target = target;
        }
    }
}
