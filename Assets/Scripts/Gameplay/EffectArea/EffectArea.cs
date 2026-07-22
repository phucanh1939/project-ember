using System.Collections.Generic;

namespace Game.Gameplay
{
    /// <summary>
    /// Owns the runtime lifecycle of a persistent effect area.
    /// Tracks valid targets, applies enter effects, ticks effects and reverts effects when targets leave.
    /// </summary>
    public class EffectArea
    {
        private readonly EntityFaction _faction;
        private readonly EntityRelation _targetMask;
        private readonly ReversibleEffect[] _enterEffects;
        private readonly Effect[] _tickEffects;
        private readonly HashSet<IEffectTarget> _targets = new();

        private readonly float _tickInterval;
        private readonly bool _hasDuration;

        private float _tickTimer;
        private float _remainingTime;

        public bool IsExpired => _hasDuration && _remainingTime <= 0f;

        public EffectArea(
            EntityFaction faction,
            EntityRelation targetMask,
            ReversibleEffect[] enterEffects,
            Effect[] tickEffects,
            float tickInterval,
            bool hasDuration,
            float duration)
        {
            _faction = faction;
            _targetMask = targetMask;
            _enterEffects = enterEffects;
            _tickEffects = tickEffects;
            _tickInterval = tickInterval;
            _hasDuration = hasDuration;
            _remainingTime = duration;
            _tickTimer = tickInterval;
        }

        public void Enter(IEffectTarget target)
        {
            if (!CanAffect(target))
                return;

            if (!_targets.Add(target))
                return;

            var context = new EffectContext(target);

            foreach (var effect in _enterEffects)
                effect.Execute(context);
        }

        public void Exit(IEffectTarget target)
        {
            if (!_targets.Remove(target))
                return;

            var context = new EffectContext(target);

            foreach (var effect in _enterEffects)
                effect.Revert(context);
        }

        public void Update(float deltaTime)
        {
            if (IsExpired)
                return;

            if (_hasDuration)
            {
                _remainingTime -= deltaTime;

                if (IsExpired)
                {
                    Expire();
                    return;
                }
            }

            if (_targets.Count == 0)
                return;

            _tickTimer -= deltaTime;

            if (_tickTimer > 0f)
                return;

            _tickTimer += _tickInterval;
            ExecuteTickEffects();
        }

        public void Expire()
        {
            foreach (var target in _targets)
            {
                var context = new EffectContext(target);

                foreach (var effect in _enterEffects)
                    effect.Revert(context);
            }

            _targets.Clear();
        }

        private void ExecuteTickEffects()
        {
            foreach (var target in _targets)
            {
                var context = new EffectContext(target);

                foreach (var effect in _tickEffects)
                    effect.Execute(context);
            }
        }

        private bool CanAffect(IEffectTarget target)
        {
            return EntityRelationUtils.CanAffect(_faction, target.Faction, _targetMask);
        }
    }
}