using UnityEngine;

namespace Game.Gameplay
{
    /// <summary>
    /// Converts character attributes into stat modifiers.
    /// </summary>
    [RequireComponent(typeof(CharacterAttributes))]
    [RequireComponent(typeof(StatModifierContainer))]
    public class AttributeModifierProvider : MonoBehaviour
    {
        [SerializeField] private AttributeStatRuleDefinition _definition;

        private CharacterAttributes _attributes;
        private StatModifierContainer _modifierContainer;

        private void Awake()
        {
            _attributes = GetComponent<CharacterAttributes>();
            _modifierContainer = GetComponent<StatModifierContainer>();

            _attributes.OnChanged += Refresh;

            Refresh();
        }

        private void OnDestroy()
        {
            if (_attributes != null)
                _attributes.OnChanged -= Refresh;
        }

        private void Refresh()
        {
            _modifierContainer.RemoveSource(StatModifierSource.Attribute);

            if (_definition == null)
                return;

            foreach (var rule in _definition.Rules)
            {
                float value = GetAttributeValue(rule.AttributeType) * rule.ValuePerPoint;
                _modifierContainer.AddModifier(new StatModifier(
                    rule.StatType,
                    StatModifierType.Flat,
                    StatModifierSource.Attribute,
                    value));
            }
        }

        private float GetAttributeValue(AttributeType type)
        {
            return type switch
            {
                AttributeType.Strength => _attributes.Strength,
                AttributeType.Dexterity => _attributes.Dexterity,
                AttributeType.Vitality => _attributes.Vitality,
                AttributeType.Energy => _attributes.Energy,
                _ => 0
            };
        }
    }
}