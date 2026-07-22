using System.Collections.Generic;
using UnityEngine;

namespace Game.Gameplay
{
    /// <summary>
    /// Represents the runtime gameplay state and behavior of a projectile.
    /// </summary>
    public class Projectile : IEntity
    {
        private readonly EntityFaction _faction;
        private readonly EntityRelation _targetMask;
        private readonly IReadOnlyList<Effect> _effects;
        private readonly float _speed;

        private float _remainingLifetime;

        public EntityFaction Faction => _faction;
        public Vector2 Position { get; private set; }
        public Vector2 Direction { get; }

        public bool IsExpired => _remainingLifetime <= 0;

        public Projectile(
            EntityFaction faction,
            EntityRelation targetMask,
            float speed,
            float lifetime,
            Vector2 position,
            Vector2 direction,
            IReadOnlyList<Effect> effects)
        {
            _faction = faction;
            _targetMask = targetMask;
            _speed = speed;
            _remainingLifetime = lifetime;
            Position = position;
            Direction = direction.normalized;
            _effects = effects;
        }

        public void Update(float deltaTime)
        {
            _remainingLifetime -= deltaTime;

            if (IsExpired)
                return;

            Position += _speed * deltaTime * Direction;
        }

        public void OnHit(IEffectTarget target)
        {
            if (!EntityRelationUtils.CanAffect(
                    _faction,
                    target.Faction,
                    _targetMask))
                return;

            var context = new EffectContext(
                target,
                Position);

            foreach (var effect in _effects)
                effect.Execute(context);
        }
    }
}