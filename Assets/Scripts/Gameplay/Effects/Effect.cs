namespace Game.Gameplay
{
    /// <summary>
    /// Runtime gameplay action that applies an immediate change.
    /// Effects execute once; persistent changes are managed by StatusEffect instances.
    /// </summary>
    public abstract class Effect
    {
        public abstract void Execute(EffectContext context);
    }
}
