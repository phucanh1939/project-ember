namespace Game.Gameplay
{
    /// <summary>
    /// Represents an object that can receive damage.
    ///
    /// Damage sources interact with this interface instead of knowing
    /// the concrete implementation behind the target.
    /// </summary>
    public interface IDamageable
    {
        void TakeDamage(DamageData damage);
    }
}