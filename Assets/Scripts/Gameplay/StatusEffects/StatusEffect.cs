namespace Game.Gameplay
{
    /// <summary>
    /// Runtime gameplay state that remains active on an entity.
    ///
    /// Unlike normal effects, status effects have a lifecycle and update over time.
    /// </summary>
    public abstract class StatusEffect
    {
        protected EffectContext _context;
        protected float _remainingTime;

        public bool IsExpired => _remainingTime <= 0;

        protected StatusEffect(EffectContext context)
        {
            _context = context;
        }

        public void Initialize(float duration)
        {
            _remainingTime = duration;
            OnApply();
        }

        public void Update(float deltaTime)
        {
            _remainingTime -= deltaTime;

            OnUpdate(deltaTime);

            if (_remainingTime <= 0)
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
    }
}