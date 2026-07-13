using UnityEngine;

namespace Game.Gameplay
{
    /// <summary>
    /// Damage receiving area of a character or object.
    ///
    /// Responsibilities:
    /// - Provides a collider used by attacks to detect hits.
    /// - Receives damage through IDamageable.
    /// - Forwards damage to the owning Health component.
    ///
    /// </summary>
    [RequireComponent(typeof(Collider2D))]
    public class Hurtbox : MonoBehaviour, IDamageable
    {
        [SerializeField] private Health _health;
        [SerializeField] private Collider2D _collider;

        private void OnValidate()
        {
            _collider = GetComponent<Collider2D>();

            if (_collider != null)
            {
                _collider.isTrigger = true;
            }
        }

        public void TakeDamage(DamageData damage)
        {
            if (_health == null)
                return;

            _health.TakeDamage(damage);
        }
    }
}