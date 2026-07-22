using System.Collections.Generic;
using UnityEngine;

namespace Game.Gameplay
{
    [CreateAssetMenu(menuName = "Gameplay/Projectiles/Projectile")]
    public class ProjectileDefinition : ScriptableObject
    {
        [SerializeField] private ProjectileController _prefab;
        [SerializeField] private EntityRelation _targetMask;
        [SerializeField] private float _speed;
        [SerializeField] private float _lifetime;
        [SerializeField] private List<EffectDefinition> _effectDefinitions;

        public ProjectileController CreateProjectile(
            IEffectInstigator instigator,
            Vector2 position,
            Vector2 direction,
            Transform parent)
        {
            var effects = new List<Effect>(_effectDefinitions.Count);

            foreach (var effectDefinition in _effectDefinitions)
                effects.Add(effectDefinition.CreateEffect(instigator));

            var projectile = new Projectile(
                instigator.Faction,
                _targetMask,
                _speed,
                _lifetime,
                position,
                direction,
                effects);

            var controller = Instantiate(
                _prefab,
                position,
                Quaternion.identity,
                parent);

            controller.Initialize(projectile);

            return controller;
        }
    }
}