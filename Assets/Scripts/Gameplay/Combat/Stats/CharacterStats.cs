using UnityEngine;

namespace Game.Gameplay
{
    /// <summary>
    /// Calculates character stats from base values and active modifiers.
    /// </summary>
    [RequireComponent(typeof(StatModifierContainer))]
    public class CharacterStats : MonoBehaviour
    {
        [Header("Base Stats")]
        [SerializeField] private int _baseHealth = 100;
        [SerializeField] private int _baseDamage = 10;
        [SerializeField] private StatModifierContainer _modifierContainer;

        private void Awake()
        {
            _modifierContainer = GetComponent<StatModifierContainer>();
        }

        public int MaxHealth => Mathf.RoundToInt(CalculateStat(_baseHealth, StatType.MaxHealth));
        public int AttackDamage => Mathf.RoundToInt(CalculateStat(_baseDamage, StatType.AttackDamage));

        private float CalculateStat(float baseValue, StatType type)
        {
            float flat = 0;
            float percent = 0;

            // PERF: Cache final values and invalidate them when modifiers change if stats are queried often.
            foreach (var modifier in _modifierContainer.GetModifiers(type))
            {
                switch (modifier.ModifierType)
                {
                    case ModifierType.Flat:
                        flat += modifier.Value;
                        break;
                    case ModifierType.Percent:
                        percent += modifier.Value;
                        break;
                }
            }

            return (baseValue + flat) * (1 + percent / 100f);
        }
    }
}
