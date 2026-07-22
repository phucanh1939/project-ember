using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Gameplay
{
    /// <summary>
    /// Immediately applies effects to all valid targets found within an area.
    /// </summary>
    public class InstantAOE : Effect
    {
        private readonly IReadOnlyList<Effect> _effects;
        private readonly AreaDefinition _area;
        private readonly AOETargetingType _targetingType;

        public InstantAOE(
            EntityFaction faction,
            EntityRelation targetMask,
            AreaDefinition area,
            AOETargetingType targetingType,
            IReadOnlyList<Effect> effects)
            : base(faction, targetMask)
        {
            _area = area;
            _targetingType = targetingType;
            _effects = effects;
        }

        protected override void OnExecute(EffectContext context)
        {
            var origin = ResolveOrigin(context);
            var direction = ResolveDirection(context);

            var colliders = _area.Query(origin, direction);

            foreach (var collider in colliders)
            {
                if (!collider.TryGetComponent<IEffectTarget>(out var target))
                    continue;

                var targetContext = new EffectContext(
                    context.Instigator,
                    target,
                    collider.transform.position);

                foreach (var effect in _effects)
                    effect.Execute(targetContext);
            }
        }

        private Vector2 ResolveOrigin(EffectContext context)
        {
            return _targetingType switch
            {
                AOETargetingType.Point =>
                    context.TargetPosition,

                AOETargetingType.Directional =>
                    context.Instigator.Position,

                _ => throw new ArgumentOutOfRangeException()
            };
        }

        private Vector2 ResolveDirection(EffectContext context)
        {
            return _targetingType switch
            {
                AOETargetingType.Point =>
                    Vector2.zero,

                AOETargetingType.Directional =>
                    (context.TargetPosition - context.Instigator.Position).normalized,

                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}
