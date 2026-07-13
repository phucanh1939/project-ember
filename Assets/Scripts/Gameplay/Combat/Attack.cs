using System;
using UnityEngine;

namespace Game.Gameplay
{
    /// <summary>
    /// Controls character attacks.
    ///
    /// Responsibilities:
    /// - Starts and ends attack actions.
    /// - Controls hitbox active windows.
    /// - Provides damage information.
    ///
    /// Does not:
    /// - Detect collisions directly.
    /// - Decide targets.
    /// - Control animation.
    ///
    /// Attack flow:
    ///
    /// </summary>
    public class Attack : MonoBehaviour
    {
        [SerializeField] private Hitbox _hitbox;
        [SerializeField] private int _damage = 10; // TODO Read it from a ScriptableObject or other data source.

        public bool IsAttacking { get; private set; }
        public event Action OnAttackEnded;

        private void Awake()
        {
            _hitbox.Disable();
        }

        public void EnableHitbox()
        {
            _hitbox.Enable();
        }

        public void DisableHitbox()
        {
            _hitbox.Disable();
        }

        public void StartAttack()
        {
            if (IsAttacking) return;
            IsAttacking = true;
            _hitbox.SetDamage(new DamageData(_damage));
        }

        public void EndAttack()
        {
            DisableHitbox();
            IsAttacking = false;
            OnAttackEnded?.Invoke();
        }

        public void CancelAttackIfActive()
        {
            if (!IsAttacking) return;
            DisableHitbox();
            IsAttacking = false;
            OnAttackEnded?.Invoke();
        }
    }
}