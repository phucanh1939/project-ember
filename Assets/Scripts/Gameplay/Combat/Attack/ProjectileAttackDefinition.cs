using UnityEngine;

namespace Game.Gameplay
{
    [CreateAssetMenu(menuName = "Game/Combat/Attack/Projectile Attack")]
    /// <summary>
    /// Executes an attack by spawning a projectile.
    /// </summary>
    public class ProjectileAttackDefinition : AttackDefinition
    {
        [SerializeField] private Projectile _projectilePrefab;

        public override void Execute(AttackContext context, WeaponDefinition weapon)
        {
            Projectile projectile = Instantiate(
                _projectilePrefab,
                context.Attacker.ProjectileSpawn.position,
                context.Attacker.ProjectileSpawn.rotation);

            projectile.Initialize(weapon.Damage,context.Attacker.Movement.FacingDirection);
        }
    }
}
