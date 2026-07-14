using UnityEngine;

namespace Game.Gameplay
{
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