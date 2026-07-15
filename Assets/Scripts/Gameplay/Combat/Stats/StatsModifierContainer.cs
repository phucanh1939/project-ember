using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Gameplay
{
    /// <summary>
    /// Stores the active runtime modifiers for a character.
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

        public IEnumerable<StatModifier> GetModifiers(StatType type)
        {
            // PERF: Index modifiers by StatType if characters accumulate many active modifiers.
            return _modifiers.Where(x => x.Type == type);
        }

        public void Clear()
        {
            _modifiers.Clear();
        }
    }
}
