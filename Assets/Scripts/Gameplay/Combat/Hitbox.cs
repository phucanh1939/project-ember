using System.Collections.Generic;
using UnityEngine;

namespace Game.Gameplay
{
    /// <summary>
    /// Detects attack hits and forwards damage to damageable targets.
    ///
    /// Responsibilities:
    /// - Detect Hurtbox collisions.
    /// - Prevent multiple hits during one attack.
    /// - Send damage to IDamageable.
    /// 
    /// 
    /// </summary>
    [RequireComponent(typeof(Collider2D))]
    public class Hitbox : MonoBehaviour
    {
        [SerializeField] private Collider2D _collider;

        // Stores targets that have already been hit during the current attack.
        // PERF:
        // Each active Hitbox stores references to targets hit during the attack.
        // This is simple and reliable, but if the game has a large number of
        // simultaneous active hitboxes, consider optimizing memory usage with
        // alternatives such as attack IDs or centralized hit tracking.
        private readonly HashSet<IDamageable> _hitTargets = new();

        private DamageData _damage;

        private void OnValidate()
        {
            _collider = GetComponent<Collider2D>();

            if (_collider != null)
                _collider.isTrigger = true;
        }

        private void Awake()
        {
            Disable();
        }

        public void SetDamage(DamageData damage)
        {
            _damage = damage;
        }

        public void Enable()
        {
            _hitTargets.Clear();
            _collider.enabled = true;
        }

        public void Disable()
        {
            _hitTargets.Clear();
            _collider.enabled = false;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.TryGetComponent<IDamageable>(out var damageable))
                return;

            if (_hitTargets.Contains(damageable))
                return;

            _hitTargets.Add(damageable);
            damageable.TakeDamage(_damage);
        }
    }
}