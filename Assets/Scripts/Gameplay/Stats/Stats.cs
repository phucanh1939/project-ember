using UnityEngine;

namespace Game.Gameplay
{
    /// <summary>
    /// Stores runtime character stats.
    ///
    /// Base stats come from CharacterStatsDefinition.
    /// Modifiers from equipment, attributes, buffs, etc. are cached until a stat changes.
    /// </summary>
    [RequireComponent(typeof(StatModifierContainer))]
    public class Stats : MonoBehaviour, IStatProvider, IMovementSpeedProvider, IMaxHealthProvider
    {
        private struct CachedStat
        {
            public float value;
            public bool dirty;
        }

        [SerializeField] private StatsDefinition _definition;
        [SerializeField] private StatModifierContainer _modifierContainer;

        private readonly float[] _baseStats = new float[(int)StatType.Count];
        private readonly CachedStat[] _cache = new CachedStat[(int)StatType.Count];

        public float MaxHealth => GetStatValue(StatType.MaxHealth);
        public float AttackDamage => GetStatValue(StatType.AttackDamage);
        public float Armor => GetStatValue(StatType.Armor);
        public float MoveSpeed => GetStatValue(StatType.MoveSpeed);
        public float CritChance => GetStatValue(StatType.CritChance);

        private void OnValidate()
        {
            _modifierContainer = GetComponent<StatModifierContainer>();
        }

        private void Awake()
        {
            InitializeBaseStats();

            _modifierContainer.OnStatChanged += MarkDirty;
        }

        private void OnDestroy()
        {
            if (_modifierContainer == null)
                return;

            _modifierContainer.OnStatChanged -= MarkDirty;
        }

        private void InitializeBaseStats()
        {
            if (_definition == null)
                return;

            SetBaseStat(StatType.MaxHealth, _definition.MaxHealth);
            SetBaseStat(StatType.AttackDamage, _definition.AttackDamage);
            SetBaseStat(StatType.Armor, _definition.Armor);
        }

        // PERF: Cache calculated values since combat systems may query stats every frame.
        public float GetStatValue(StatType type)
        {
            int index = (int)type;

            if (!_cache[index].dirty)
                return _cache[index].value;

            float value = _baseStats[index];

            foreach (var modifiers in _modifierContainer.GetModifiers().Values)
            {
                foreach (var modifier in modifiers)
                {
                    if (modifier.StatType != type)
                        continue;

                    value = modifier.Apply(value);
                }
            }

            _cache[index].value = value;
            _cache[index].dirty = false;

            return value;
        }

        private void SetBaseStat(StatType type, float value)
        {
            int index = (int)type;

            _baseStats[index] = value;
            _cache[index].dirty = true;
        }

        private void MarkDirty(StatType type)
        {
            _cache[(int)type].dirty = true;
        }

        private void MarkAllDirty()
        {
            for (int i = 0; i < _cache.Length; i++)
                _cache[i].dirty = true;
        }
    }
}