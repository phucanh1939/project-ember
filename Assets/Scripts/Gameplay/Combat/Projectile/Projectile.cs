using System.Collections.Generic;
using UnityEngine;

namespace Game.Gameplay
{
    /// <summary>
    /// Represents a projectile in the game world.
    /// Moves, detects collisions, and executes effects on impact.
    /// </summary>
    [RequireComponent(typeof(Collider2D))]
    public class Projectile : MonoBehaviour, IEntity
    {
        private EntityFaction _faction;
        private EntityRelation _targetMask;
        private IReadOnlyList<Effect> _effects;
        private float _speed;
        private float _remainingLifetime;
        private Vector2 _direction;

        public EntityFaction Faction => _faction;

        public void Initialize(EntityFaction faction, EntityRelation targetMask, float speed, float lifetime, Vector2 direction, IReadOnlyList<Effect> effects)
        {
            _faction = faction;
            _targetMask = targetMask;
            _speed = speed;
            _remainingLifetime = lifetime;
            _direction = direction.normalized;
            _effects = effects;
        }

        private void Update()
        {
            _remainingLifetime -= Time.deltaTime;

            if (_remainingLifetime <= 0)
            {
                Destroy(gameObject);
                return;
            }

            transform.position += (Vector3)(_speed * Time.deltaTime * _direction);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.TryGetComponent<IEffectTarget>(out var target))
                return;

            if (!EntityRelationUtils.CanAffect(_faction, target.Faction, _targetMask))
                return;

            var context = new EffectContext(target, transform.position, Vector2.zero, null);

            foreach (var effect in _effects)
                effect.Execute(context);

            Destroy(gameObject);
        }
    }
}