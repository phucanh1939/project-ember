namespace Game.Gameplay
{
    /// <summary>
    /// Runtime gameplay state that remains active on an entity.
    ///
    /// Unlike normal effects, status effects have a lifecycle and update over time.
    /// </summary>
    public abstract class StatusEffect
    {
        protected EffectContext context;

        protected float remainingTime;

        protected StatusEffect(EffectContext context)
        {
            this.context = context;
        }

        public void Initialize(float duration)
        {
            remainingTime = duration;
            OnApply();
        }

        public void Update(float deltaTime)
        {
            remainingTime -= deltaTime;

            OnUpdate(deltaTime);

            if (remainingTime <= 0)
                OnExpire();
        }

        protected virtual void OnApply()
        {
        }

        protected virtual void OnUpdate(float deltaTime)
        {
        }

        protected virtual void OnExpire()
        {
        }

        public bool IsExpired => remainingTime <= 0;
    }
}