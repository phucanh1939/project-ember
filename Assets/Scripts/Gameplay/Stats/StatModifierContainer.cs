using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Gameplay
{
    /// <summary>
    /// Stores active runtime stat modifiers grouped by their source.
    /// </summary>
    public class StatModifierContainer : MonoBehaviour, IStatModifierReceiver
    {
        private readonly Dictionary<int, List<StatModifier>> _modifiers = new();

        private int _nextModifierId;

        public event Action<StatType> OnStatChanged;

        public int AddModifier(StatModifier modifier)
        {
            int id = ++_nextModifierId;

            _modifiers.Add(id, new List<StatModifier> { modifier });

            OnStatChanged?.Invoke(modifier.StatType);

            return id;
        }

        public int AddModifiers(IReadOnlyList<StatModifier> modifiers)
        {
            int id = ++_nextModifierId;

            _modifiers.Add(id, new List<StatModifier>(modifiers));

            foreach (var modifier in modifiers)
                OnStatChanged?.Invoke(modifier.StatType);

            return id;
        }

        public void RemoveModifiers(int id)
        {
            if (!_modifiers.Remove(id, out var modifiers))
                return;

            foreach (var modifier in modifiers)
                OnStatChanged?.Invoke(modifier.StatType);
        }

        public IReadOnlyDictionary<int, List<StatModifier>> GetModifiers()
        {
            return _modifiers;
        }
    }
}