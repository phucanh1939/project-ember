using System.Collections.Generic;
using UnityEngine;

namespace Game.Gameplay
{
    /// <summary>
    /// Manages active status effects on an entity.
    /// </summary>
    public class StatusEffectController : MonoBehaviour
    {
        private readonly List<StatusEffect> _activeEffects = new();

        public void Add(StatusEffectDefinition definition, EffectContext context)
        {
            StatusEffect instance = definition.CreateInstance(context);
            instance.Initialize(definition.Duration);
            _activeEffects.Add(instance);
        }

        private void Update()
        {
            for (int i = _activeEffects.Count - 1; i >= 0; i--)
            {
                _activeEffects[i].Update(Time.deltaTime);

                // Remove expired effects.
                if (_activeEffects[i].IsExpired)
                    _activeEffects.RemoveAt(i);
            }
        }
    }
}
