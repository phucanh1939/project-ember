using System.Collections.Generic;
using UnityEngine;

namespace Game.Gameplay
{
    /// <summary>
    /// Applies effects immediately to all targets found inside an area.
    /// </summary>
    public class InstantAOE : Effect
    {
        private readonly AreaDefinition _area;
        private readonly IReadOnlyList<Effect> _effects;

        public InstantAOE(
            AreaDefinition area,
            IReadOnlyList<Effect> effects)
        {
            _area = area;
            _effects = effects;
        }

        public override void Execute(EffectContext context)
        {
            var position = context.OriginPosition;
            var direction = context.Direction;
            var colliders = _area.Query(position, direction);
            foreach (var collider in colliders)
            {
                if (!collider.TryGetComponent<IEffectTarget>(out var target))
                    continue;
                var targetContext = new EffectContext(context.Instigator, target, collider.transform.position, Vector2.zero);
                foreach (var effect in _effects)
                    effect.Execute(targetContext);
            }
        }
    }
}