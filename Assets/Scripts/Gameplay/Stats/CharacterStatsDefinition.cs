using UnityEngine;

namespace Game.Gameplay
{
    [CreateAssetMenu(fileName = "CharacterStatsDefinition", menuName = "Game/Stats/Character Stats Definition")]
    public class CharacterStatsDefinition : ScriptableObject
    {
        [SerializeField] private float _maxHealth = 100;
        [SerializeField] private float _attackDamage = 10;
        [SerializeField] private float _armor = 0;
        [SerializeField] private float _moveSpeed = 0;

        public float MaxHealth => _maxHealth;
        public float AttackDamage => _attackDamage;
        public float Armor => _armor;
        public float MoveSpeed => _moveSpeed;
    }
}