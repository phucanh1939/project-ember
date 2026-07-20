using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Gameplay
{
    /// <summary>
    /// Stores active runtime stat modifiers from different gameplay systems.
    /// </summary>
    public class StatModifierContainer : MonoBehaviour
    {
        // Each list contains all active modifiers affecting that stat.
        // Indexed by StatType because stat calculation is the most frequent operation.
        // This avoids scanning unrelated modifiers and keeps GetFinalStat() proportional
        // to the number of modifiers affecting that stat.
        private readonly List<StatModifier>[] _modifiers = new List<StatModifier>[(int)StatType.Count];

        public event Action<StatType> OnStatChanged;
        public event Action OnAllStatsChanged;

        private void Awake()
        {
            for (int i = 0; i < _modifiers.Length; i++)
                _modifiers[i] = new List<StatModifier>();
        }

        public void AddModifier(StatModifier modifier)
        {
            _modifiers[(int)modifier.StatType].Add(modifier);

            OnStatChanged?.Invoke(modifier.StatType);
        }

        public void RemoveModifier(StatModifier modifier)
        {
            if (!_modifiers[(int)modifier.StatType].Remove(modifier))
                return;

            OnStatChanged?.Invoke(modifier.StatType);
        }

        public void RemoveSource(StatModifierSource source)
        {
            for (int i = 0; i < _modifiers.Length; i++)
            {
                int removed = _modifiers[i].RemoveAll(x => x.Source == source);

                if (removed > 0)
                    OnStatChanged?.Invoke((StatType)i);
            }
        }

        public IReadOnlyList<StatModifier> GetModifiers(StatType type)
        {
            return _modifiers[(int)type];
        }

        public void Clear()
        {
            bool changed = false;

            foreach (var modifiers in _modifiers)
            {
                if (modifiers.Count == 0)
                    continue;

                modifiers.Clear();
                changed = true;
            }

            if (changed)
                OnAllStatsChanged?.Invoke();
        }
    }
}