# Pure ECS Game World with Unity Adapters

## 1. Overview

The game is divided into two separate worlds:

```text
┌─────────────────────────────────────┐
│            PURE GAME WORLD          │
│                                     │
│  ECS                                 │
│  Entities                            │
│  Components                          │
│  Systems                             │
│  Factories                           │
│  Commands                            │
│  Events                              │
│                                     │
│  No Unity references                 │
└──────────────────┬──────────────────┘
                   │
            Commands / Events
                   │
┌──────────────────▼──────────────────┐
│            UNITY WORLD              │
│                                     │
│  GameObjects                         │
│  MonoBehaviours                      │
│  Rigidbody2D                         │
│  Collider2D                          │
│  Animator                            │
│  VFX                                 │
│  Audio                               │
│  Unity Input System                  │
└─────────────────────────────────────┘
```

The Pure Game World owns the gameplay simulation.

The Unity World provides platform services:

* Input
* Physics
* Rendering
* Animation
* VFX
* Audio
* Scene objects

The two worlds communicate through explicit data.

```text
Pure World ───── Commands ─────▶ Unity World

Pure World ◀── Results / Events ─ Unity World
```

The Pure World must not directly depend on:

```csharp
GameObject
MonoBehaviour
Transform
Rigidbody2D
Collider2D
Animator
```

---

# 2. Core Architecture

The core architecture is:

```text
GameBootstrap
        │
        ▼
    GameWorld
        │
        ├── EntityManager
        │
        ├── ComponentStorage
        │
        ├── Systems
        │
        ├── Factories
        │
        ├── Command Buffers
        │
        └── Event Buffers
```

The Unity side:

```text
GameWorldRunner
        │
        ├── InputAdapter
        ├── PhysicsAdapter
        ├── RenderSync
        ├── AudioAdapter
        └── VFXAdapter
```

---

# 3. ECS Core

The ECS has three fundamental concepts:

```text
Entity
    = Identity

Component
    = Data

System
    = Behavior
```

## 3.1 Entity

An entity is only an identifier.

```csharp
public readonly struct EntityId
{
    public int Value { get; }

    public EntityId(int value)
    {
        Value = value;
    }
}
```

The entity itself does not contain behavior.

It does not know:

```csharp
Move()
TakeDamage()
Update()
Attack()
```

Instead:

```text
EntityId
    ↓
Components
    ↓
Systems process them
```

Example:

```text
Entity 1
├── Position
├── Movement
├── Health
└── PlayerTag

Entity 2
├── Position
├── Movement
├── Health
└── EnemyTag

Entity 3
├── Position
├── Projectile
└── Lifetime
```

---

# 4. Components

Components are pure runtime data.

Example:

```csharp
public struct Position
{
    public Vector2 Value;
}
```

```csharp
public struct Movement
{
    public Vector2 Direction;
    public float Speed;
}
```

```csharp
public struct Health
{
    public float Current;
    public float Max;
}
```

```csharp
public struct Lifetime
{
    public float Remaining;
}
```

Components should describe runtime state.

They should not contain:

```csharp
MonoBehaviour
GameObject
Transform
Rigidbody2D
```

or gameplay behavior.

---

## 4.1 Component roles

Different components serve different purposes.

### Data Components

Contain values:

```text
Position
Movement
Health
Stats
Cooldown
Lifetime
```

### Tag Components

Identify a category:

```csharp
public struct PlayerTag
{
}
```

```csharp
public struct EnemyTag
{
}
```

```csharp
public struct ProjectileTag
{
}
```

Tags contain no data.

They allow systems to distinguish entities.

Example:

```text
Position + Movement + PlayerTag
    → Player-controlled entity

Position + Movement + EnemyTag
    → AI-controlled entity
```

### State Components

Store current state:

```csharp
public struct CharacterState
{
    public CharacterStateId Current;
}
```

### Relationship Components

Store references to other entities using IDs:

```csharp
public struct Target
{
    public EntityId Value;
}
```

The Pure World should use:

```text
EntityId
```

instead of references to Unity objects.

---

# 5. Component Storage

The `EntityManager` owns entity and component storage.

Conceptually:

```text
EntityManager
│
├── Entity IDs
│
├── Position Storage
│
├── Movement Storage
│
├── Health Storage
│
└── Other Component Storage
```

The first implementation can be simple:

```text
EntityId
    ↓
ComponentStorage<T>
```

For example:

```csharp
ComponentStorage<Position>
ComponentStorage<Movement>
ComponentStorage<Health>
```

The storage is responsible for:

```text
Add component
Remove component
Get component
Check component existence
Query entities
```

The system should not care how the storage is internally implemented.

Later, the storage can evolve from:

```text
Dictionary<EntityId, T>
```

to:

```text
Dense arrays
```

and eventually:

```text
Archetypes
    ↓
Chunks
    ↓
Contiguous component memory
```

The ECS architecture should remain the same while storage becomes more optimized.

---

# 6. Systems

Systems contain gameplay behavior.

Example:

```text
MovementSystem
HealthSystem
CombatSystem
ProjectileSystem
StatusEffectSystem
CooldownSystem
LifetimeSystem
```

A system queries the components it needs.

For example:

```text
MovementSystem
    ↓
Query:
    Movement
    Position
```

The system processes matching entities.

```csharp
public sealed class MovementSystem
{
    public void Update(
        EntityManager entities,
        CommandBuffer commands)
    {
        foreach (var entity in entities.Query<Movement>())
        {
            ref var movement =
                ref entities.Get<Movement>(entity);

            var velocity =
                movement.Direction *
                movement.Speed;

            commands.Add(
                new SetVelocityCommand(
                    entity,
                    velocity));
        }
    }
}
```

The system does not directly manipulate Unity objects.

It produces a command for the Unity Physics Adapter.

---

# 7. GameWorld

The `GameWorld` owns the pure simulation.

```csharp
public sealed class GameWorld
{
    public EntityManager Entities { get; }

    private readonly InputSystem _inputSystem;
    private readonly MovementSystem _movementSystem;
    private readonly EnemySystem _enemySystem;
    private readonly ProjectileSystem _projectileSystem;

    private readonly CommandBuffer _commands;
    private readonly EventBuffer _events;
}
```

The world controls system execution order.

For example:

```text
GameWorld.Update()
        │
        ├── Process external input
        │
        ├── Process physics results
        │
        ├── Process gameplay events
        │
        ├── Run AI systems
        │
        ├── Run movement systems
        │
        ├── Run combat systems
        │
        ├── Run lifetime systems
        │
        └── Produce commands/events
```

The important distinction is:

```text
GameWorld
    = owns simulation and execution order

System
    = owns a specific gameplay behavior

EntityManager
    = owns entity/component storage

Factory
    = creates entity composition
```

---

# 8. Bootstrap

`GameBootstrap` creates and connects the systems.

The setup flow is:

```text
GameBootstrap
        │
        ├── Create EntityManager
        │
        ├── Create ComponentStorage
        │
        ├── Create CommandBuffers
        │
        ├── Create EventBuffers
        │
        ├── Create Factories
        │
        ├── Create Systems
        │
        ├── Create GameWorld
        │
        ├── Create Unity Adapters
        │
        └── Create GameWorldRunner
```

Conceptually:

```csharp
public sealed class GameBootstrap : MonoBehaviour
{
    private void Awake()
    {
        var entities =
            new EntityManager();

        var commands =
            new CommandBuffer();

        var events =
            new EventBuffer();

        var movementSystem =
            new MovementSystem();

        var projectileSystem =
            new ProjectileSystem();

        var world =
            new GameWorld(
                entities,
                movementSystem,
                projectileSystem,
                commands,
                events);

        // Connect Unity adapters to the world.
    }
}
```

The bootstrap is the composition root.

It is responsible for:

```text
Create
    ↓
Connect
    ↓
Initialize
```

It should not contain gameplay logic.

---

# 9. Entity Creation

The pure world owns gameplay entity creation.

Factories create pure entities.

Example:

```text
EnemyDefinition
        ↓
EnemyFactory
        ↓
EntityManager.CreateEntity()
        ↓
Add components
        ↓
Pure Entity exists
```

Example:

```csharp
public sealed class EnemyFactory
{
    private readonly EntityManager _entities;

    public EntityId Create(
        EnemyDefinition definition,
        Vector2 position)
    {
        var entity =
            _entities.CreateEntity();

        _entities.Add(
            entity,
            new Position
            {
                Value = position
            });

        _entities.Add(
            entity,
            new Health
            {
                Current = definition.MaxHealth,
                Max = definition.MaxHealth
            });

        _entities.Add(
            entity,
            new EnemyTag());

        return entity;
    }
}
```

The factory creates:

```text
Pure Entity
    +
Components
```

It does not directly instantiate:

```csharp
GameObject
MonoBehaviour
Rigidbody2D
```

---

# 10. Who Creates the Unity Representation?

The Pure World owns the entity.

The Unity World owns its representation.

```text
Pure Entity
    ↕
Unity Representation
```

Example:

```text
EntityId 42
    ↕
EnemyView
    ↕
GameObject
    ├── Transform
    ├── Rigidbody2D
    ├── Collider2D
    └── SpriteRenderer
```

The entity-to-Unity mapping is maintained by the Unity side:

```text
EntityId
    ↔
Unity Representation
```

A possible flow:

```text
Pure World
    ↓
EntityCreatedEvent
    ↓
Unity EntityViewSystem
    ↓
Instantiate prefab
    ↓
Register EntityId ↔ View
```

For example:

```csharp
public readonly struct EntityCreatedEvent
{
    public EntityId Entity;
}
```

The Unity side receives:

```text
EntityCreatedEvent
    ↓
ViewFactory
    ↓
Instantiate prefab
    ↓
Register EntityId
```

The Pure World does not know which prefab was created.

This keeps the two worlds independent.

---

# 11. Entity Destruction

The reverse process is:

```text
Pure World
    ↓
EntityDestroyedEvent
    ↓
Unity EntityViewSystem
    ↓
Destroy Unity Representation
    ↓
Remove EntityId Mapping
```

Example:

```text
Entity 42
    ↓
DestroyEntity
    ↓
EntityDestroyedEvent
    ↓
Destroy GameObject
```

The ownership is:

```text
Pure World:
    Entity lifetime

Unity World:
    Unity representation lifetime
```

---

# 12. GameWorldRunner

The runner connects both worlds.

It is responsible for the update loop.

Conceptually:

```text
Unity Frame
    │
    ▼
GameWorldRunner
    │
    ├── Read Input
    │
    ├── Send Input to Pure World
    │
    ├── Send Physics Results to Pure World
    │
    ├── Update Pure World
    │
    ├── Read Commands
    │
    ├── Apply Commands to Unity
    │
    └── Process Render Events
```

The runner is an orchestrator.

It should not contain gameplay rules.

---

# 13. Render Synchronization

There are two types of synchronization.

## 13.1 Continuous state synchronization

For example:

```text
Pure Position
        ↓
RenderSync
        ↓
Unity Transform
```

However, when Unity Physics owns the physical position:

```text
Unity Rigidbody Position
        ↓
Physics Adapter
        ↓
Pure Position
        ↓
RenderSync
```

The source of truth must be clear.

For a physics-driven entity:

```text
Unity Physics
    = physical position authority
```

For a purely visual entity:

```text
Pure World
    = position authority
```

---

## 13.2 Event-based synchronization

The Pure World produces gameplay events:

```text
AttackStarted
EntityDied
AbilityUsed
StatusEffectApplied
ProjectileCreated
```

Example:

```csharp
public readonly struct AttackStartedEvent
{
    public EntityId Entity;
}
```

The Pure World:

```text
AttackSystem
    ↓
AttackStartedEvent
    ↓
EventBuffer
```

The Unity side:

```text
EventBuffer
    ↓
Render / Animation System
    ↓
Animator.Play(...)
```

The Pure World says:

```text
"An attack started."
```

The Unity side decides:

```text
"Play this animation."
```

This prevents gameplay logic from depending on animation implementation.

---

# 14. Render Sync Flow

Example:

```text
Pure World
    │
    ├── EntityCreatedEvent
    │       ↓
    │   Create View
    │
    ├── EntityDestroyedEvent
    │       ↓
    │   Destroy View
    │
    ├── AttackStartedEvent
    │       ↓
    │   Play Animation
    │
    ├── EntityDiedEvent
    │       ↓
    │   Play Death VFX
    │
    └── Position State
            ↓
        Sync Transform
```

The Unity side owns:

```text
GameObject
Transform
Animator
SpriteRenderer
VFX
AudioSource
```

The Pure World owns:

```text
Gameplay meaning
```

---

# 15. Physics Adapter

Unity Physics is treated as an external simulation service.

The Pure World sends commands:

```text
Pure World
    ↓
SetVelocityCommand
    ↓
PhysicsAdapter
    ↓
Rigidbody2D
```

The Unity Physics system then executes:

```text
Velocity
    ↓
Collision
    ↓
Collision Resolution
    ↓
Actual Position
```

The adapter sends results back:

```text
Unity Physics
    ↓
PhysicsAdapter
    ├── PhysicsStateUpdate
    └── CollisionEvent
    ↓
Pure World
```

---

# 16. Physics State

A physics state update represents the actual physical result.

```csharp
public readonly struct PhysicsStateUpdate
{
    public EntityId Entity;
    public Vector2 Position;
    public Vector2 Velocity;
}
```

The flow:

```text
Rigidbody2D
    ↓
Read actual position
    ↓
PhysicsStateUpdate
    ↓
GameWorld
    ↓
Update Position component
```

The Pure World does not independently predict the final position if Unity Physics is authoritative.

It receives the actual result.

---

# 17. Physics Events

Physics events represent things that happened.

```csharp
public readonly struct CollisionEvent
{
    public EntityId EntityA;
    public EntityId EntityB;
}
```

Unity:

```text
OnTriggerEnter2D
        ↓
PhysicsAdapter
        ↓
CollisionEvent
        ↓
PhysicsEventBuffer
        ↓
GameWorld
```

The physics callback must not directly execute gameplay:

```csharp
// Avoid
private void OnTriggerEnter2D(Collider2D other)
{
    target.TakeDamage();
}
```

Instead:

```text
Unity Physics
    ↓
CollisionEvent
    ↓
Pure World
    ↓
CombatSystem
    ↓
Damage
```

This keeps the flow predictable.

---

# 18. Movement

Movement is split into:

```text
Movement Intent
        ↓
Movement Logic
        ↓
Physics Command
        ↓
Unity Physics
        ↓
Physics Result
```

Example:

```text
Input
    ↓
Movement.Direction
    ↓
MovementSystem
    ↓
Desired Velocity
    ↓
SetVelocityCommand
    ↓
Rigidbody2D.linearVelocity
```

The Pure World calculates:

```text
Desired Velocity
```

The Unity Physics layer calculates:

```text
Actual Movement
```

because collisions may alter the result.

Example:

```text
Pure World:
    Desired velocity = (5, 0)

Unity Physics:
    Entity hits wall

Actual result:
    Position = (10, 5)
    Velocity = (0, 0)
```

The Pure World then receives:

```text
PhysicsStateUpdate
```

and updates its state.

This prevents:

```text
Pure Position ≠ Unity Position
```

---

# 19. Input Adapter

The Unity Input System is not used directly by gameplay systems.

The flow is:

```text
Keyboard / Controller
        ↓
Unity Input System
        ↓
InputAdapter
        ↓
InputData
        ↓
Pure GameWorld
        ↓
InputSystem
```

Example:

```csharp
public struct PlayerInput
{
    public Vector2 Movement;
    public bool AttackPressed;
}
```

The Unity adapter reads:

```text
Keyboard
    ↓
Movement = (1, 0)
```

and sends:

```text
PlayerInput
{
    Movement = (1, 0)
}
```

to the Pure World.

The Pure World does not know whether the input came from:

```text
Keyboard
Controller
Touchscreen
AI
Replay
Network
```

It only receives input data.

---

# 20. Player Entity vs AI Entity

The important insight is that the player and AI do not need completely different entity types.

They can share the same gameplay components.

For example:

```text
Player Entity
├── Position
├── Movement
├── Health
├── Combat
└── PlayerTag
```

```text
Enemy Entity
├── Position
├── Movement
├── Health
├── Combat
└── EnemyTag
```

Both can be processed by:

```text
MovementSystem
HealthSystem
CombatSystem
```

The difference is how their intent is produced.

---

## 20.1 Player Input Flow

```text
Unity Input
    ↓
InputAdapter
    ↓
PlayerInput
    ↓
PlayerInputSystem
    ↓
Movement.Direction
```

```text
PlayerInput
    ↓
Movement Intent
```

---

## 20.2 AI Input Flow

```text
AI System
    ↓
Target
    ↓
Decision
    ↓
Movement Intent
```

```text
Enemy AI
    ↓
Movement.Direction
```

Both eventually produce the same component data:

```text
Movement.Direction
```

Then:

```text
MovementSystem
    ↓
Movement.Direction
    ↓
Desired Velocity
```

This is an important separation:

```text
Input Source
    ↓
Intent
    ↓
Movement
    ↓
Physics
```

The movement system should not care whether the direction came from:

```text
Player
AI
Replay
Network
```

---

# 21. Shared Gameplay Pipeline

Player:

```text
Input
    ↓
PlayerInputSystem
    ↓
Movement Intent
    ↓
MovementSystem
    ↓
Velocity Command
    ↓
Unity Physics
```

Enemy:

```text
AI Decision
    ↓
EnemyAISystem
    ↓
Movement Intent
    ↓
MovementSystem
    ↓
Velocity Command
    ↓
Unity Physics
```

The shared part is:

```text
Movement Intent
    ↓
MovementSystem
    ↓
Physics
```

Only the intent-generation system differs.

---

# 22. Commands

Commands represent actions requested from another layer.

Example:

```csharp
public readonly struct SetVelocityCommand
{
    public EntityId Entity;
    public Vector2 Velocity;
}
```

Pure World:

```text
MovementSystem
    ↓
SetVelocityCommand
```

Unity:

```text
SetVelocityCommand
    ↓
PhysicsAdapter
    ↓
Rigidbody2D
```

Commands are useful for:

```text
SetVelocity
SpawnEntity
DestroyEntity
PlayAnimation
PlayVFX
PlayAudio
```

However, commands should be used carefully.

A command should describe:

> Something another system or world must do.

---

# 23. Events

Events describe something that already happened.

Examples:

```text
CollisionEvent
EntityDiedEvent
AttackStartedEvent
AbilityUsedEvent
EntityCreatedEvent
EntityDestroyedEvent
```

The difference:

```text
Command:
    "Do this."

Event:
    "This happened."
```

Example:

```text
MovementSystem
    ↓
SetVelocityCommand
    ↓
Unity Physics
```

```text
Unity Physics
    ↓
CollisionEvent
    ↓
Pure World
```

---

# 24. Entity Lifecycle

A complete entity lifecycle is:

```text
Definition
    ↓
Factory
    ↓
Create Entity
    ↓
Add Components
    ↓
EntityCreatedEvent
    ↓
Unity View / Physics Representation
    ↓
Entity Active
```

Destruction:

```text
Gameplay Logic
    ↓
Destroy Entity
    ↓
Remove Entity from ECS
    ↓
EntityDestroyedEvent
    ↓
Destroy Unity Representation
```

The Pure World owns the entity lifecycle.

The Unity World owns the Unity representation lifecycle.

---

# 25. ScriptableObject Definitions

ScriptableObjects are configuration.

```text
ProjectileDefinition
EnemyDefinition
AbilityDefinition
EffectDefinition
ItemDefinition
```

They are not runtime entities.

The flow is:

```text
ScriptableObject Definition
        ↓
Factory
        ↓
Pure Runtime Entity
        ↓
Components
```

For example:

```text
ProjectileDefinition
    ↓
ProjectileFactory
    ↓
EntityId
    ├── Position
    ├── Movement
    ├── Projectile
    └── Lifetime
```

Runtime data should contain the values needed by gameplay.

It should not unnecessarily hold references to configuration assets.

For references between definitions, IDs can be used:

```text
ItemDefinition
    ↓
AbilityId
```

Then:

```text
AbilityId
    ↓
DefinitionRegistry
    ↓
AbilityDefinition
    ↓
AbilityFactory
    ↓
Runtime Ability
```

---

# 26. Recommended Update Flow

A complete frame can be structured like this:

```text
┌────────────────────────────────────┐
│            UNITY UPDATE            │
└──────────────────┬─────────────────┘
                   │
                   ▼
            Read Input
                   │
                   ▼
         Input Adapter Buffer
                   │
                   ▼
┌────────────────────────────────────┐
│           PURE GAME WORLD          │
│                                    │
│  1. Consume Input                  │
│                                    │
│  2. Consume Physics Results        │
│                                    │
│  3. Process AI                     │
│                                    │
│  4. Process Player Intent          │
│                                    │
│  5. Process Gameplay Systems       │
│                                    │
│  6. Generate Commands              │
│                                    │
│  7. Generate Events                │
└──────────────────┬─────────────────┘
                   │
                   ▼
            Read Commands
                   │
                   ▼
        Apply to Unity World
                   │
                   ▼
            Unity Physics
                   │
                   ▼
        Collect Physics Results
                   │
                   ▼
         Send Results to World
```

The exact timing can later be refined around `Update` and `FixedUpdate`.

The important thing is to define a stable simulation order.

---

# 27. Ownership Rules

The architecture should have clear ownership.

## Pure Game World owns

```text
Entity identity
Component data
Gameplay state
Gameplay rules
AI decisions
Combat results
Ability execution
Status effects
Cooldowns
Entity lifetime
```

## Unity World owns

```text
GameObjects
Transforms
Rigidbody2D
Colliders
Unity Input
Animation
Rendering
VFX
Audio
```

## Physics Adapter owns

```text
EntityId ↔ Rigidbody2D mapping
Applying physics commands
Reading physics state
Converting Unity callbacks to pure events
```

## Render Sync owns

```text
EntityId ↔ View mapping
Creating views
Destroying views
Synchronizing visual state
Processing visual events
```

## GameWorldRunner owns

```text
Update orchestration
Moving data between worlds
Calling the Pure World
Calling adapters
```

The runner should not own gameplay rules.

---

# 28. Final Architecture

```text
                           GAME BOOTSTRAP
                                  │
                 ┌────────────────┴────────────────┐
                 │                                 │
                 ▼                                 ▼
          PURE GAME WORLD                    UNITY WORLD
                 │                                 │
        ┌────────┴────────┐              ┌─────────┴─────────┐
        │                 │              │                   │
   EntityManager      Systems       InputAdapter       PhysicsAdapter
        │                 │              │                   │
        ▼                 ▼              ▼                   ▼
   Components       Gameplay       InputData          Rigidbody2D
   Storage           Logic                              Physics
        │                 │                                  │
        │                 └────────── Commands ──────────────┘
        │
        └────────────── Events / Results ◀───────────────────┐
                                                              │
                                                       Unity Physics
                                                              │
                                                              ▼
                                                        Physics Events
```

Rendering:

```text
Pure World
    │
    ├── Entity State
    │       ↓
    │   Render Sync
    │       ↓
    │   Unity View
    │
    └── Gameplay Events
            ↓
        Event Buffer
            ↓
        Unity Presentation
            ├── Animation
            ├── VFX
            ├── Audio
            └── UI
```

---

# 29. Core Principles

### 1. Entity is identity

```text
Entity = EntityId
```

### 2. Components are data

```text
Component = Runtime State
```

### 3. Systems are behavior

```text
System = Logic
```

### 4. Factories create entity composition

```text
Factory
    = Entity + Components
```

### 5. Pure World owns gameplay

```text
Gameplay state
    → Pure World
```

### 6. Unity owns platform services

```text
Physics
Input
Rendering
Animation
Audio
    → Unity World
```

### 7. Commands request actions

```text
"Do this."
```

### 8. Events report results

```text
"This happened."
```

### 9. Physics has one authority

If Unity Physics is used for movement:

```text
Unity Physics
    = physical truth
```

### 10. Player and AI share gameplay pipelines

```text
Player Input ──┐
               ├── Intent
AI Decision ───┘
                  ↓
            MovementSystem
                  ↓
              Physics
```

The input source changes.

The gameplay pipeline does not need to.

---

# 30. The Direction of the Project

The architecture should evolve in this order:

```text
1. Build a small custom ECS core
        ↓
2. Create pure gameplay components
        ↓
3. Create systems
        ↓
4. Create factories
        ↓
5. Create GameWorld
        ↓
6. Add Unity adapters
        ↓
7. Add Input Adapter
        ↓
8. Add Physics Adapter
        ↓
9. Add Render Synchronization
        ↓
10. Build gameplay features
        ↓
11. Optimize ECS storage when needed
```

The goal is not to immediately build a production-grade ECS.

The goal is to build a clear simulation architecture where:

```text
Game Logic
    ≠
Unity Representation
```

and:

```text
Entity
    = ID

Component
    = Data

System
    = Behavior

Factory
    = Creation

Adapter
    = External World Integration
```

This gives the project a clean foundation that can later evolve from a simple custom ECS into a more optimized data-oriented architecture without changing the fundamental gameplay model.
