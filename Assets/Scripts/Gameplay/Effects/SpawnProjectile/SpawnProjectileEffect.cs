namespace Game.Gameplay
{
    /// <summary>
    /// Spawns a projectile from the effect instigator.
    /// </summary>
    public class SpawnProjectileEffect : Effect
    {
        private readonly ProjectileDefinition _definition;

        public SpawnProjectileEffect(EntityFaction faction, EntityRelation targetMask, ProjectileDefinition definition)
            : base(faction, targetMask)
        {
            _definition = definition;
        }

        protected override void OnExecute(EffectContext context)
        {
            context.Instigator.ProjectileSpawner.Spawn(_definition, context.TargetPosition);
        }
    }
}