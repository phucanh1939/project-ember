using UnityEngine;

namespace Game.Gameplay
{
    /// <summary>
    /// Defines the execution lifecycle of a weapon attack.
    /// </summary>
    public abstract class AttackDefinition : ScriptableObject
    {
        public abstract void Execute(AttackContext context, WeaponDefinition weapon);

        public virtual void Complete(AttackContext context, WeaponDefinition weapon)
        {
        }

        public virtual void End(AttackContext context, WeaponDefinition weapon)
        {
        }

        public virtual void Cancel(AttackContext context, WeaponDefinition weapon)
        {
            Complete(context, weapon);
            End(context, weapon);
        }
    }
}
