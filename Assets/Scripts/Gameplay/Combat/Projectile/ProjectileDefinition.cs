using System.Collections.Generic;
using UnityEngine;

namespace Game.Gameplay
{
    /// <summary>
    /// Defines the configuration of a projectile.
    /// </summary>
    [CreateAssetMenu(menuName = "Gameplay/Projectiles/Projectile")]
    public class ProjectileDefinition : ScriptableObject
    {
        [SerializeField] private Projectile _prefab;
        [SerializeField] private EntityRelation _targetMask;
        [SerializeField] private float _speed;
        [SerializeField] private float _lifetime;
        [SerializeField] private List<EffectDefinition> _effectDefinitions;

        public Projectile CreateProjectile(IEffectInstigator instigator, Vector2 position, Vector2 direction, Transform parent)
        {
            var effects = new List<Effect>(_effectDefinitions.Count);

            foreach (var effectDefinition in _effectDefinitions)
                effects.Add(effectDefinition.CreateEffect(instigator));

            var projectile = Instantiate(_prefab, position, Quaternion.identity, parent);
            projectile.Initialize(instigator.Faction, _targetMask, _speed, _lifetime, direction, effects);

            return projectile;
        }
    }
}