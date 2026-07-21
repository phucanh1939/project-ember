# TODO

- [ ] Attribute
- [ ] Stats
- [ ] Effects
- [ ] Projectile
- [ ] Effect Zone
- [ ] Ability
- [ ] Damge Calculatetion
- [ ] Items

## Stats
- [x] Implement dirty set and cache for Character Stats reading

## Damage
- [x] DamageType
- [x] DamageData
- [ ] Damage Deal Calculation
- [ ] Damage Taken Calculation

## Effects
- [x] EffectDefinition (config)
- [x] Effect (runtime)
- [x] DamageEffect
- [ ] Pooling

## Status Effects
- [x] StatusEffectDefinition (config)
- [x] StatusEffect (runtime)
- [x] StatusEffectController (managing active status effects on a character)
- [x] ApplyStatusEffect Effect
- [x] DamageOverTime
- [x] Slow
- [x] Stun
- [ ] Pooling

## Projectile
- [x] Projectile Config
- [x] Runtime: data + logic
- [x] ProjectileSpawner
- [x] SpawnProjectileEffect
- [ ] Projectile types
- [ ] Pooling

## EffectZone
- [ ] EffectZone types
- [ ] EffectZoneDefinition (config)
- [ ] EffectZone (runtime)
- [ ] Pooling

## Ability
- [ ] AbilityDefinition
- [ ] Ability(runtime)
- [ ] AbilityController (managing active abilities on a character)
- [ ] Pooling

## Items
- [ ] Item Config (with 2 type consumable and equipment)
- [ ] Consumable Item Logic
- [ ] Eqiupment Logic

## Dead
- [ ] Dead State for Enemy
- [ ] Dead State for Player
- [ ] Respawn Player

## Pooling:
- [ ] Projectile
- [ ] Effects
- [ ] Status Effect
- [ ] Ability
- [ ] Enemy
