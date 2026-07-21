namespace Game.Gameplay
{
    /// <summary>
    /// Runtime gameplay state that remains active on an entity.
    ///
    /// Unlike normal effects, status effects have a lifecycle and update over time.
    /// </summary>
    public abstract class StatusEffect
    {
        private float _remainingTime;

        public bool IsExpired => _remainingTime <= 0;

        protected StatusEffect(float duration)
        {
            _remainingTime = duration;
        }

        public void Apply(IEffectTarget target)
        {
            OnApply(target);
        }

        public void Update(float deltaTime, IEffectTarget target)
        {
            _remainingTime -= deltaTime;

            OnUpdate(deltaTime, target);

            if (_remainingTime <= 0)
                OnExpire(target);
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