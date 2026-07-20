using UnityEngine;

namespace Game.Gameplay
{
    [CreateAssetMenu(menuName = "Game/Combat/Attack/Melee Attack")]
    /// <summary>
    /// Executes an attack by enabling the attacker's hitbox.
    /// </summary>
    public class MeleeAttackDefinition : AttackDefinition
    {
        [SerializeField] private HitboxDefinition _hitbox;

        public override void Execute(AttackContext context, WeaponDefinition weapon)
        {
            Hitbox hitbox = context.Attacker.Hitbox;
            hitbox.Setup(_hitbox);
            hitbox.SetDamage(new DamageData(weapon.Damage));
            hitbox.Enable();
        }

        public override void Complete(AttackContext context, WeaponDefinition weapon)
        {
            context.Attacker.Hitbox.Disable();
        }

        public override void Cancel(AttackContext context, WeaponDefinition weapon)
        {
            context.Attacker.Hitbox.Disable();
        }
    }
}
