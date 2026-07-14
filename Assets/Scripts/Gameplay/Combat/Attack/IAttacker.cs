using UnityEngine;

namespace Game.Gameplay
{
    public interface IAttacker
    {
        Transform Transform { get; }

        Movement Movement { get; }

        WeaponHolder WeaponHolder { get; }

        Hitbox Hitbox { get; }

        Transform ProjectileSpawn { get; }
    }
}
