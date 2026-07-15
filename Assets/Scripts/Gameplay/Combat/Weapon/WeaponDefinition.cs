using UnityEngine;

namespace Game.Gameplay
{
    [CreateAssetMenu(menuName = "Game/Weapon")]
    /// <summary>
    /// Defines a weapon's base damage and basic attack.
    /// </summary>
    public class WeaponDefinition : ScriptableObject
    {
        [Header("Weapon Info")]
        [SerializeField] private string _weaponName;

        [Header("Stats")]
        [SerializeField] private int _damage = 10;

        [Header("Attacks")]
        [SerializeField] private AttackDefinition _basicAttack;

        public string WeaponName => _weaponName;

        public int Damage => _damage;

        public AttackDefinition BasicAttack => _basicAttack;
    }
}
