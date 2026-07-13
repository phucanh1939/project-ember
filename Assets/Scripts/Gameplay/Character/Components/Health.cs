using UnityEngine;

namespace Game.Gameplay
{
    /// <summary>
    /// Stores and manages a character's health.
    ///
    /// RESPONSIBILITIES:
    /// - Store current and maximum health.
    /// - Apply damage.
    /// - Restore health.
    /// - Report whether the character is alive.
    ///
    /// ARCH:
    /// This is a reusable gameplay component.
    /// It contains no Player, Enemy, or UI specific logic.
    /// Other systems observe or react to health changes.
    /// </summary>
    public class Health : MonoBehaviour
    {
        [SerializeField]
        [Min(1)]
        private int maxHealth = 100;

        public int MaxHealth => maxHealth;

        public int CurrentHealth { get; private set; }

        public bool IsAlive => CurrentHealth > 0;

        private void Awake()
        {
            CurrentHealth = maxHealth;
        }

        /// <summary>
        /// Reduces health by the specified amount.
        /// </summary>
        public void TakeDamage(DamageData damageData)
        {
            if (!IsAlive)
                return;

            CurrentHealth = Mathf.Max(CurrentHealth - damageData.Amount, 0);

            if (CurrentHealth <= 0)
            {
                Die();
            }
        }

        /// <summary>
        /// Restores health by the specified amount.
        /// </summary>
        public void Heal(int amount)
        {
            if (!IsAlive)
                return;

            CurrentHealth = Mathf.Min(CurrentHealth + amount, maxHealth);
        }

        private void Die()
        {
            Debug.Log($"{name} died");
        }
    }
}