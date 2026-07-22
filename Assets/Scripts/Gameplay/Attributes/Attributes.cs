using System;
using UnityEngine;

namespace Game.Gameplay
{
    /// <summary>
    /// Runtime character attributes.
    ///
    /// Stores progression values such as Strength, Dexterity, Vitality, and Energy.
    /// Does not calculate combat stats.
    /// </summary>
    public class Attributes : MonoBehaviour
    {
        [SerializeField] private AttributesDefinition _definition;

        public int Strength { get; private set; }
        public int Dexterity { get; private set; }
        public int Vitality { get; private set; }
        public int Energy { get; private set; }

        public event Action OnChanged;

        private void Awake()
        {
            Initialize();
        }

        private void Initialize()
        {
            if (_definition == null)
                return;

            Strength = _definition.Strength;
            Dexterity = _definition.Dexterity;
            Vitality = _definition.Vitality;
            Energy = _definition.Energy;
        }

        public void AddStrength(int amount)
        {
            Strength += amount;
            OnChanged?.Invoke();
        }

        public void AddDexterity(int amount)
        {
            Dexterity += amount;
            OnChanged?.Invoke();
        }

        public void AddVitality(int amount)
        {
            Vitality += amount;
            OnChanged?.Invoke();
        }

        public void AddEnergy(int amount)
        {
            Energy += amount;
            OnChanged?.Invoke();
        }
    }
}