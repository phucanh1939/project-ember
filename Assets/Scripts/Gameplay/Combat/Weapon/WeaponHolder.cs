using UnityEngine;

namespace Game.Gameplay
{
    /// <summary>
    /// Stores the weapon currently equipped by a character.
    /// </summary>
    public class WeaponHolder : MonoBehaviour
    {
        [SerializeField]
        private WeaponDefinition _startingWeapon;

        public WeaponDefinition CurrentWeapon { get; private set; }

        private void Awake()
        {
            Equip(_startingWeapon);
        }

        public void Equip(WeaponDefinition weapon)
        {
            CurrentWeapon = weapon;
        }
    }
}
