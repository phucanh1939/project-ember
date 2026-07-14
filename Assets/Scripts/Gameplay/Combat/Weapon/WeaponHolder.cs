using UnityEngine;

namespace Game.Gameplay
{
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