using UnityEngine;

namespace Game.Gameplay
{
    [CreateAssetMenu(fileName = "StatsDefinition", menuName = "Gameplay/Stats/Stats Definition")]
    public class StatsDefinition : ScriptableObject
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