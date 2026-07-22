using UnityEngine;

namespace Game.Gameplay
{
    /// <summary>
    /// Defines the configuration used to create a persistent runtime effect area.
    /// </summary>
    [CreateAssetMenu(menuName = "Gameplay/Effects/Effect Area")]
    public class EffectAreaDefinition : ScriptableObject
    {
        [Header("Targeting")]
        [SerializeField] private EntityFaction _faction;
        [SerializeField] private EntityRelation _targetMask;

        [Header("Effects")]
        [SerializeField] private ReversibleEffectDefinition[] _enterEffects;
        [SerializeField] private EffectDefinition[] _tickEffects;

        [Header("Timing")]
        [SerializeField] private float _tickInterval = 1f;
        [SerializeField] private bool _hasDuration = true;
        [SerializeField] private float _duration = 5f;

        public EffectArea CreateEffectArea(IEffectInstigator instigator)
        {
            var enterEffects = new ReversibleEffect[_enterEffects.Length];

            for (var i = 0; i < _enterEffects.Length; i++)
                enterEffects[i] = _enterEffects[i].CreateReversibleEffect(instigator);

            var tickEffects = new Effect[_tickEffects.Length];

            for (var i = 0; i < _tickEffects.Length; i++)
                tickEffects[i] = _tickEffects[i].CreateEffect(instigator);

            return new EffectArea(
                _faction,
                _targetMask,
                enterEffects,
                tickEffects,
                _tickInterval,
                _hasDuration,
                _duration);
        }
    }
}