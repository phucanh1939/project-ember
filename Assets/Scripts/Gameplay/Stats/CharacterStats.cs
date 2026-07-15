using System.Collections.Generic;
using UnityEngine;

namespace Game.Gameplay
{
    /// <summary>
    /// Stores runtime character stats.
    ///
    /// Base stats come from CharacterStatsDefinition.
    /// Modifiers from equipment, attributes, buffs, etc. are applied at query time.
    /// </summary>
    [RequireComponent(typeof(StatModifierContainer))]
    public class CharacterStats : MonoBehaviour
    {
        [SerializeField] private CharacterStatsDefinition _definition;
        [SerializeField] private StatModifierContainer _modifierContainer;

        private readonly Dictionary<StatType, float> _baseStats = new();

        public int MaxHealth => Mathf.RoundToInt(GetFinalStat(StatType.MaxHealth));
        public int AttackDamage => Mathf.RoundToInt(GetFinalStat(StatType.AttackDamage));
        public int Armor => Mathf.RoundToInt(GetFinalStat(StatType.Armor));

        private void OnValidate()
        {
            _modifierContainer = GetComponent<StatModifierContainer>();
        }

        private void Awake()
        {
            InitializeBaseStats();
        }

        private void InitializeBaseStats()
        {
            if (_definition == null)
                return;

            _baseStats[StatType.MaxHealth] = _definition.MaxHealth;
            _baseStats[StatType.AttackDamage] = _definition.AttackDamage;
            _baseStats[StatType.Armor] = _definition.Armor;
        }

        private float GetFinalStat(StatType type)
        {
            if (!_baseStats.TryGetValue(type, out var value))
                return 0;

            foreach (var modifier in _modifierContainer.GetModifiers(type))
                value = modifier.Apply(value);

            return value;
        }
    }
}