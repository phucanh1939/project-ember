using System.Collections.Generic;
using UnityEngine;

namespace Game.Gameplay
{
    /// <summary>
    /// Manages active status effects on an entity.
    /// </summary>
    public class StatusEffectController : MonoBehaviour, IStatusEffectReceiver
    {
        private readonly List<StatusEffect> _activeEffects = new();
        private IEffectTarget _target;

        public void Initialize(IEffectTarget target)
        {
            _target = target;
        }

        public void AddStatusEffect(StatusEffect statusEffect)
        {
            statusEffect.Apply(_target);
            _activeEffects.Add(statusEffect);
        }

        private void Update()
        {
            for (int i = _activeEffects.Count - 1; i >= 0; i--)
            {
                _activeEffects[i].Update(Time.deltaTime, _target);

                // Remove expired effects.
                if (_activeEffects[i].IsExpired)
                    _activeEffects.RemoveAt(i);
            }
        }
    }
}
