using UnityEngine;

namespace Game.Gameplay
{
    [CreateAssetMenu(fileName = "CharacterAttributesDefinition", menuName = "Game/Stats/Character Attributes Definition")]
    public class CharacterAttributesDefinition : ScriptableObject
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