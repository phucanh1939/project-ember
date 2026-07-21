using System.Collections.Generic;
using UnityEngine;

namespace Game.Gameplay
{
    /// <summary>
    /// Defines effects to apply to all targets found inside an area immediately.
    /// </summary>
    [CreateAssetMenu(menuName = "Gameplay/Effects/Instant AOE")]
    public class InstantAOEDefinition : EffectDefinition
    {
        [SerializeField] private AreaDefinition _area;
        [SerializeField] private List<EffectDefinition> _effectDefinitions;

        public override Effect CreateEffect(IEffectInstigator instigator)
        {
            var effects = new List<Effect>(_effectDefinitions.Count);

            foreach (var effectDefinition in _effectDefinitions)
                effects.Add(effectDefinition.CreateEffect(instigator));

            return new InstantAOE(instigator.Faction, _targetMask, _area, effects);
        }
    }
}