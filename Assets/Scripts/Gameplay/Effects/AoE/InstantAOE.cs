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

        public InstantAOE(EntityFaction faction, EntityRelation targetMask, AreaDefinition area, IReadOnlyList<Effect> effects)
            : base(faction, targetMask)
        {
            _area = area;
            _effects = effects;
        }

        protected override void OnExecute(EffectContext context)
        {
            var colliders = _area.Query(context.OriginPosition, context.Direction);

            foreach (var collider in colliders)
            {
                if (!collider.TryGetComponent<IEffectTarget>(out var target))
                    continue;

                var targetContext = new EffectContext(target, collider.transform.position, Vector2.zero, null);

                foreach (var effect in _effects)
                    effect.Execute(targetContext);
            }
        }
    }
}