using System.Collections.Generic;
using UnityEngine;

namespace Game.Gameplay
{
    /// <summary>
    /// Defines how character attributes convert into stat modifiers.
    /// </summary>
    [CreateAssetMenu(fileName = "AttributeStatRuleDefinition", menuName = "Gameplay/Stats/Attribute Stat Rule Definition")]
    public class AttributeStatRuleDefinition : ScriptableObject
    {
        [SerializeField] private List<AttributeStatRule> _rules = new();

        public IReadOnlyList<AttributeStatRule> Rules => _rules;
    }
}