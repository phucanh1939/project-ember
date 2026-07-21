using System;
using UnityEngine;

namespace Game.Gameplay
{
    /// <summary>
    /// Tracks current health using the character's calculated maximum health.
    /// </summary>
    [RequireComponent(typeof(CharacterStats))]
    public class Health : MonoBehaviour
    {
        private IMaxHealthProvider _maxHealthProvider;

        public float CurrentHealth { get; private set; }
        public float MaxHealth => _maxHealthProvider.MaxHealth;
        public bool IsAlive => CurrentHealth > 0;

        public event Action OnDeath;

        public void Initialize(IMaxHealthProvider maxHealthProvider)
        {
            _maxHealthProvider = maxHealthProvider;
            CurrentHealth = _maxHealthProvider.MaxHealth;
        }

        public void TakeDamage(DamageData damage)
        {
            if (!IsAlive)
                return;

            CurrentHealth = Mathf.Max(CurrentHealth - damage.damage, 0);

            if (CurrentHealth == 0)
            {
                Die();
            }
        }

        public void Heal(int amount)
        {
            if (!IsAlive)
                return;

            CurrentHealth = Mathf.Min(CurrentHealth + amount, _maxHealthProvider.MaxHealth);
        }

        private void Die()
        {
            OnDeath?.Invoke();
        }
    }
}
