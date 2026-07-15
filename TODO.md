# TODO

## Combat integration

- [ ] Update the Player and Enemy prefabs to include and configure `Stats` and `StatModifierContainer` components.
- [ ] Create an `AnimationEventReceiverBridge` on the visual/Animator child object. It forwards animation events to root-level gameplay components because the Animator is not on the character root.
  - [ ] Forward attack execute event to `Attack.ExecuteAttack()`.
  - [ ] Forward attack-complete event to `Attack.CompleteAttack()`.
  - [ ] Forward attack-end event to `Attack.EndAttack()`.
- [ ] Add the attack animation events to each directional player attack clip: execute during the hit frame, complete when the hit window ends, and end when the animation finishes.
- [ ] Add equivalent attack animation events/bridge support for enemies when enemy attacks are enabled.
- [ ] Implement a concrete root-level `IAttacker` adapter for Player and Enemy, exposing the owner transform, movement, weapon holder, hitbox, and projectile spawn point.
- [ ] Create combat ScriptableObject assets: `WeaponDefinition`, `MeleeAttackDefinition`/`ProjectileAttackDefinition`, and `HitboxDefinition`.
- [ ] Add and configure `WeaponHolder` on Player and Enemy prefabs; assign their starting weapons.
- [ ] Reconcile the Player and Slime prefab serialization with the refactored `Attack` component. The prefabs still hold obsolete `_hitbox`/`_damage` values instead of the current weapon-based configuration.
- [ ] Verify the hitbox/hurtbox layers and Physics 2D collision matrix so attacks can damage opponents but not unintended targets.

## Death and state-machine completion

- [ ] Add an Enemy `DeadState` and a matching `DeadStateDefinition`; include it in every enemy AI config so `Health.OnDeath` has a registered destination state.
- [ ] Define enemy death behavior: stop movement and attacks, disable AI/sensor/colliders as appropriate, play a death animation/effect, then despawn or destroy the enemy.
- [ ] Complete Player `DeadState`: stop movement, cancel active attacks, disable player input/interaction, and play death presentation.
- [ ] Decide and implement player death recovery (restart checkpoint, reload scene, or respawn flow).
- [ ] Implement actual `StatusEffect` behavior and state mappings for stun and knockback, or defer the interrupt subscriptions until those states exist.

## AI and gameplay configuration

- [ ] Move enemy AI tuning out of runtime state classes and into state definitions/config: attack range, leash range, idle duration, wander radius, and return arrival distance.
- [ ] Add validation/error reporting for missing required combat and AI configuration (weapon holder, starting weapon, attack definition, attacker adapter, and registered states).
- [ ] Remove temporary debug logging from state transitions and player attack input once combat debugging is complete.
