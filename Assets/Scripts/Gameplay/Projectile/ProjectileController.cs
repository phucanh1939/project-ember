using UnityEngine;

namespace Game.Gameplay
{
    [RequireComponent(typeof(Collider2D))]
    public class ProjectileController : MonoBehaviour
    {
        private Projectile _projectile;

        public void Initialize(Projectile projectile)
        {
            _projectile = projectile;
            transform.position = projectile.Position;
        }

        private void Update()
        {
            _projectile.Update(Time.deltaTime);

            if (_projectile.IsExpired)
            {
                Destroy(gameObject);
                return;
            }

            transform.position = _projectile.Position;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.TryGetComponent<IEffectTarget>(out var target))
                return;

            _projectile.OnHit(target);

            Destroy(gameObject);
        }
    }
}