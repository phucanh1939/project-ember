using UnityEngine;

namespace Game.Gameplay
{
    /// <summary>
    /// Contains temporary runtime information required to execute an effect.
    /// </summary>
    public readonly struct EffectContext
    {
        public IEffectInstigator Instigator { get; }
        public IEffectTarget Target { get; }
        public Vector2 AimPosition { get; }

        public EffectContext(IEffectInstigator instigator, IEffectTarget target, Vector2 aimPosition)
        {
            Instigator = instigator;
            Target = target;
            AimPosition = aimPosition;
        }
    }
}