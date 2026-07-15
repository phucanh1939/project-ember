# TODO

## Misc

- [ ] Remove Debug Logs

## Animation

- [ ] Setup player attack animations and blendtree
- [ ] Setup enemy attack animations and blend tree
- [ ] Create an `AnimationEventReceiverBridge` to receiver animation events and call needed function from Root (Player and Enemy)
- [ ] Create attack events: Execute, Complete, End

# Combat

- [ ] Implement IAttacker for PlayerController and Enemy Controller, setup needed things (like projectile Spawn root)
- [ ] Create combat ScriptableObject assets: `WeaponDefinition`, `MeleeAttackDefinition`/`ProjectileAttackDefinition`, and `HitboxDefinition`.
- [ ] Add and configure `WeaponHolder` on Player and Enemy prefabs; assign their starting weapons.
- [ ] Verify the hitbox/hurtbox layers and Physics 2D collision matrix so attacks can damage opponents but not unintended targets.
- [ ] Hitbox editor for `HitboxDefinition`

## Stats

- [ ] Create character config for each Character Class (Attribute Config and Stats Config)
- [ ] Create enemy config for each type of enemy (Stats Config)
- [ ] Setup Player Prefab: Add and setup stat and attribute components
- [ ] Setup Enemy Prefab: Add and setup stat components
