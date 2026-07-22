using System.Collections.Generic;
using UnityEngine;

namespace Game.Gameplay
{
    /// <summary>
    /// Defines an effect that immediately applies effects to all valid targets
    /// found within an area.
    /// </summary>
    [CreateAssetMenu(menuName = "Gameplay/Effects/Instant AOE")]
    public class InstantAOEDefinition : EffectDefinition
    {
        [SerializeField] private AreaDefinition _area;
        [SerializeField] private AOETargetingType _targetingType;
        [SerializeField] private List<EffectDefinition> _effectDefinitions;

        public override Effect CreateEffect(IEffectInstigator instigator)
        {
            var effects = new List<Effect>(_effectDefinitions.Count);

            foreach (var effectDefinition in _effectDefinitions)
                effects.Add(effectDefinition.CreateEffect(instigator));

            return new InstantAOE(
                instigator.Faction,
                _targetMask,
                _area,
                _targetingType,
                effects);
        }
    }
}