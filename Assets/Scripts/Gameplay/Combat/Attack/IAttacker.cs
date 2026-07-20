using UnityEngine;

namespace Game.Gameplay
{
    /// <summary>
    /// Exposes character data required to execute an attack.
    /// </summary>
    public interface IAttacker
    {
        Transform Transform { get; }

        Movement Movement { get; }

        WeaponHolder WeaponHolder { get; }

        Hitbox Hitbox { get; }

        Transform ProjectileSpawn { get; }
    }
}
