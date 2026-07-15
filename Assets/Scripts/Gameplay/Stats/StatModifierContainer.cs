using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Gameplay
{
    /// <summary>
    /// Stores active runtime stat modifiers from different gameplay systems.
    /// </summary>
    public class StatModifierContainer : MonoBehaviour
    {
        private readonly List<StatModifier> _modifiers = new();

        public void AddModifier(StatModifier modifier)
        {
            _modifiers.Add(modifier);
        }

        public void RemoveModifier(StatModifier modifier)
        {
            _modifiers.Remove(modifier);
        }

        public void RemoveSource(StatModifierSource source)
        {
            _modifiers.RemoveAll(x => x.Source == source);
        }

        public IEnumerable<StatModifier> GetModifiers(StatType type)
        {
            // PERF: Index modifiers by StatType if characters accumulate many active modifiers.
            return _modifiers.Where(x => x.StatType == type);
        }

        public void Clear()
        {
            _modifiers.Clear();
        }
    }
}