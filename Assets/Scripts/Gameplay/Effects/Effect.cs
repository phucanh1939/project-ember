using UnityEngine;

namespace Game.Gameplay
{
    /// <summary>
    /// Represents a runtime gameplay action.
    ///
    /// Effects do not maintain a lifecycle like StatusEffects.
    /// However, a runtime instance may need to be created before execution
    /// to capture values that must remain stable until the effect is applied.
    ///
    /// Example:
    /// A projectile deals 50 + 20% of the caster's ATK on hit.
    /// The damage is calculated when the projectile is spawned (effect creation),
    /// stored in the runtime effect, and applied when the projectile hits (effect execution).
    /// </summary>
    public abstract class Effect : IEntity
    {
        protected EntityFaction _faction;
        protected EntityRelation _targetMask;

        public EntityFaction Faction => _faction;

        protected Effect(EntityFaction faction, EntityRelation targetMask)
        {
            _faction = faction;
            _targetMask = targetMask;
        }

        public void Execute(EffectContext context)
        {
            if (!CanExecute(context))
                return;

            OnExecute(context);
        }

        protected virtual bool CanExecute(EffectContext context)
        {
            if (context.Target == null) return true; // Non target effect (like AOE, Spawn porjectile, etc)
            return EntityRelationUtils.CanAffect(_faction, context.Target.Faction, _targetMask);
        }

        protected abstract void OnExecute(EffectContext context);
    }
}
