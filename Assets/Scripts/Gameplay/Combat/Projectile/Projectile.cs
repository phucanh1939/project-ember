using System.Collections.Generic;
using UnityEngine;

namespace Game.Gameplay
{
    /// <summary>
    /// Represents a projectile in the game world.
    /// Moves, detects collisions, and executes effects on impact.
    /// </summary>
    [RequireComponent(typeof(Collider2D))]
    public class Projectile : MonoBehaviour
    {
        private IReadOnlyList<Effect> _effects;

        private float _speed;
        private float _remainingLifetime;
        private Vector2 _direction;

        public void Initialize(
            float speed,
            float lifetime,
            Vector2 direction,
            IReadOnlyList<Effect> effects)
        {
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

            transform.position += (Vector3)(_direction * _speed * Time.deltaTime);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.TryGetComponent<IEffectTarget>(out var target))
                return;

            var context = new EffectContext(null, target, transform.position, Vector2.zero);

            foreach (var effect in _effects)
                effect.Execute(context);

            Destroy(gameObject);
        }
    }
}