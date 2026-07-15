using UnityEngine;

namespace Game.Gameplay
{
    [CreateAssetMenu(fileName = "CharacterStatsDefinition", menuName = "Game/Stats/Character Stats Definition")]
    public class CharacterStatsDefinition : ScriptableObject
    {
        [SerializeField] private int _maxHealth = 100;
        [SerializeField] private int _attackDamage = 10;
        [SerializeField] private int _armor = 0;

        public int MaxHealth => _maxHealth;
        public int AttackDamage => _attackDamage;
        public int Armor => _armor;
    }
}