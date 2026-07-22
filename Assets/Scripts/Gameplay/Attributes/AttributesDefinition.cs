using UnityEngine;

namespace Game.Gameplay
{
    [CreateAssetMenu(fileName = "AttributesDefinition", menuName = "Gameplay/Stats/Attributes Definition")]
    public class AttributesDefinition : ScriptableObject
    {
        [SerializeField] private int _strength = 10;
        [SerializeField] private int _dexterity = 10;
        [SerializeField] private int _vitality = 10;
        [SerializeField] private int _energy = 10;

        public int Strength => _strength;
        public int Dexterity => _dexterity;
        public int Vitality => _vitality;
        public int Energy => _energy;
    }
}