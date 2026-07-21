namespace Game.Gameplay
{
    /// <summary>
    /// Spawns a projectile from the effect instigator.
    /// </summary>
    public class SpawnProjectileEffect : Effect
    {
        private readonly ProjectileSpawner _projectileSpawner;
        private readonly ProjectileDefinition _definition;

        public SpawnProjectileEffect(
            ProjectileSpawner projectileSpawner,
            ProjectileDefinition definition)
        {
            _projectileSpawner = projectileSpawner;
            _definition = definition;
        }

        public override void Execute(EffectContext context)
        {
            _projectileSpawner.Spawn(_definition, context.TargetPosition);
        }
    }
}
