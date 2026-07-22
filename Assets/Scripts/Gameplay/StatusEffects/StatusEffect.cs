namespace Game.Gameplay
{
    /// <summary>
    /// Runtime gameplay state that remains active on an entity.
    ///
    /// Unlike normal effects, status effects have a lifecycle and update over time.
    /// </summary>
    public abstract class StatusEffect : IEntity
    {
        protected EntityFaction _faction;
        protected EntityRelation _targetMask;

        private float _remainingTime;

        public EntityFaction Faction => _faction;
        public bool IsExpired => _remainingTime <= 0;

        protected StatusEffect(EntityFaction faction, EntityRelation targetMask, float duration)
        {
            _faction = faction;
            _targetMask = targetMask;
            _remainingTime = duration;
        }

        public void Apply(IEffectTarget target)
        {
            if (!CanAffect(target))
                return;

            OnApply(target);
        }

        public void Update(float deltaTime, IEffectTarget target)
        {
            if (!CanAffect(target))
                return;

            _remainingTime -= deltaTime;

            OnUpdate(deltaTime, target);

            if (_remainingTime <= 0)
                OnExpire(target);
        }

        protected virtual bool CanAffect(IEffectTarget target)
        {
            return EntityRelationUtils.CanAffect(_faction, target.Faction, _targetMask);
        }

        protected virtual void OnApply(IEffectTarget target)
        {
        }

        protected virtual void OnUpdate(float deltaTime, IEffectTarget target)
        {
        }

        protected virtual void OnExpire(IEffectTarget target)
        {
        }
    }
}