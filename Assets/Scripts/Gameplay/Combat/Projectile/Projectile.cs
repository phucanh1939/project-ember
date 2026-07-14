using UnityEngine;

namespace Game.Gameplay
{
    [RequireComponent(typeof(Collider2D))]
    public class Projectile : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private float _speed = 10f;
        [SerializeField] private float _lifetime = 5f;

        private Vector2 _direction;
        private DamageData _damage;

        public void Initialize(int damage, Vector2 direction)
        {
            _damage = new DamageData(damage);
            _direction = direction.normalized;

            Destroy(gameObject, _lifetime);
        }

        private void Update()
        {
            transform.position += (Vector3)(_direction * _speed * Time.deltaTime);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.TryGetComponent(out Hurtbox hurtbox))
                return;

            hurtbox.TakeDamage(_damage);

            Destroy(gameObject);
        }
    }
}
