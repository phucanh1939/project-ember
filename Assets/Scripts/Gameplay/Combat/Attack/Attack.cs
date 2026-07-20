using System;
using UnityEngine;

namespace Game.Gameplay
{
    /// <summary>
    /// Controls the attack lifecycle.
    ///
    /// Responsibilities:
    /// - Starts and ends attacks.
    /// - Tracks whether an attack is in progress.
    /// - Executes the current attack at animation events.
    ///
    /// Does not:
    /// - Calculate damage.
    /// - Control hitboxes.
    /// - Spawn projectiles.
    /// - Decide attack behavior.
    /// </summary>
    [RequireComponent(typeof(WeaponHolder))]
    public class Attack : MonoBehaviour
    {
        [SerializeField] private WeaponHolder _weaponHolder;

        private IAttacker _attacker;

        private WeaponDefinition _currentWeapon;
        private AttackDefinition _currentAttack;
        private AttackContext _context;

        public bool IsAttacking { get; private set; }

        public event Action OnAttackEnded;
        public event Action OnAttackCanceled;

        private void OnValidate()
        {
            _weaponHolder = GetComponent<WeaponHolder>();
        }

        private void Awake()
        {
            _attacker = GetComponent<IAttacker>();
            _context = new AttackContext(_attacker);
        }

        public void StartAttack()
        {
            if (IsAttacking)
                return;

            _currentWeapon = _weaponHolder.CurrentWeapon;

            if (_currentWeapon == null)
                return;

            _currentAttack = _currentWeapon.BasicAttack;

            IsAttacking = true;
        }

        /// <summary>
        /// Animation Event.
        /// Executes the attack effect (enable hitbox, spawn projectile, etc.).
        /// </summary>
        public void ExecuteAttack()
        {
            _currentAttack?.Execute(_context, _currentWeapon);
        }

        /// <summary>
        /// Animation Event.
        /// Ends the attack execution (disable hitbox, stop beam, etc.).
        /// </summary>
        public void CompleteAttack()
        {
            _currentAttack?.Complete(_context, _currentWeapon);
        }

        /// <summary>
        /// Animation Event.
        /// Ends the attack lifecycle.
        /// </summary>
        public void EndAttack()
        {
            _currentAttack?.End(_context, _currentWeapon);

            _currentWeapon = null;
            _currentAttack = null;

            IsAttacking = false;
            OnAttackEnded?.Invoke();
        }

        public void CancelAttackIfActive()
        {
            if (!IsAttacking)
                return;

            _currentAttack?.Cancel(_context, _currentWeapon);

            _currentWeapon = null;
            _currentAttack = null;

            IsAttacking = false;
            OnAttackCanceled?.Invoke();
        }
    }
}