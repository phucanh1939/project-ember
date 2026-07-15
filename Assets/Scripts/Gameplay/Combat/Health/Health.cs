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
        private CharacterStats _stats;

        public int CurrentHealth { get; private set; }
        public int MaxHealth => _stats.MaxHealth;
        public bool IsAlive => CurrentHealth > 0;

        public event Action OnDeath;

        private void Awake()
        {
            _stats = GetComponent<CharacterStats>();
            CurrentHealth = MaxHealth;
        }

        public void TakeDamage(DamageData damage)
        {
            if (!IsAlive)
                return;

            CurrentHealth = Mathf.Max(CurrentHealth - damage.Amount, 0);

            if (CurrentHealth == 0)
            {
                Die();
            }
        }

        public void Heal(int amount)
        {
            if (!IsAlive)
                return;

            CurrentHealth = Mathf.Min(CurrentHealth + amount, MaxHealth);
        }

        private void Die()
        {
            OnDeath?.Invoke();
        }
    }
}
