# Project Context - 2D Top-Down Action RPG

## Project Overview

This project is a **2D top-down Action RPG** built with **Unity** and **C#**.

The gameplay is inspired by classic ARPGs such as **Diablo II**, but the world is **handcrafted** (no procedural generation for now).

The primary goal of the project is **learning game architecture while building a complete game**. We intentionally start with conventional Unity architecture and only optimize after the game is feature complete.

---

# Development Philosophy

The project follows these principles:

* Build one complete feature at a time.
* Keep every milestone playable.
* Favor clean architecture over premature optimization.
* Separate decision-making from execution.
* Optimize only after everything works.

The project should first resemble a typical professional Unity project before evolving toward a more data-oriented architecture.

---

# High-Level Architecture

The project is divided into several layers.

```text
Core
    ↓
Gameplay
    ↓
Player / Enemy / NPC
    ↓
UI
```

## Core

Responsible for how the game runs.

Examples:

* Bootstrap
* Scene Management
* Camera
* Audio
* Save / Load
* Input
* Utilities

Core never contains gameplay logic.

Core services are instantiated by a dedicated **Core.prefab** loaded from the Bootstrap scene.

```text
Bootstrap Scene
        │
        ▼
Instantiate Core.prefab
        │
        ▼
Core
├── InputService
├── SceneLoader
├── Camera
└── ...
```

The Core root has a dedicated `DontDestroy` component.

Individual services should not call `DontDestroyOnLoad()` themselves.

---

## Gameplay

Shared gameplay systems.

Gameplay systems define **how mechanics work**, not **who uses them**.

Examples:

* Movement
* Combat
* Interaction
* Health

These systems are shared by:

* Player
* Enemy
* NPC

---

## Player

The Player layer is responsible only for **decision making**.

It contains:

* PlayerController
* PlayerStateMachine
* Player States

It never implements movement or combat.

Instead it uses Gameplay systems.

---

## Enemy

Enemy follows the same architecture as Player.

Enemy AI decides behavior.

Gameplay systems execute actions.

---

# Character Architecture

Every character follows the same architecture.

```text
Controller
    ↓
State Machine
    ↓
Current State
    ↓
Gameplay Systems
```

Gameplay systems execute actions.

Controllers make decisions.

---

# Character Prefab Architecture

Player and Enemy share the same prefab layout.

```text
Character
│
├── Controller
├── StateMachine
├── Movement
├── Health
├── Rigidbody2D
├── Collider2D
│
└── Visual
    ├── SpriteRenderer
    ├── Animator
    ├── CharacterAnimation
    └── SpriteDirection
```

Player uses:

```text
PlayerController
PlayerStateMachine
```

Enemy uses:

```text
EnemyController
EnemyStateMachine
```

---

# Gameplay Component Philosophy

Gameplay systems belong under:

```text
Scripts/Gameplay/
```

They are reusable by:

* Player
* Enemy
* NPC

Controllers should never duplicate gameplay logic.

---

# Gameplay Components

## Movement

Responsible for:

* Rigidbody2D movement
* Velocity
* FacingDirection

Movement never:

* Reads player input
* Makes AI decisions
* Plays animations

Movement exposes:

```text
Velocity
FacingDirection
```

Movement publishes gameplay events.

Currently:

```text
OnFacingDirectionChanged
```

Presentation systems subscribe to these events.

---

## Health

Responsible only for storing and modifying health.

Future systems (combat, UI, death, etc.) will react through events when needed.

---

## CharacterAnimation

CharacterAnimation observes Movement.

Responsibilities:

* Update Animator parameters

```text
MoveX
MoveY
Speed
```

It never chooses animation clips directly.

The Animator Controller determines which animation to play.

---

## SpriteDirection

SpriteDirection is responsible only for sprite orientation.

Responsibilities:

* Listen to Movement.OnFacingDirectionChanged
* Flip SpriteRenderer

It should remain a focused component.

Future visual features should become separate components.

Examples:

* CharacterVFX
* EquipmentVisual
* CharacterMaterialEffects

---

# Event-Driven Philosophy

Gameplay owns state.

Presentation reacts.

Example:

```text
Movement
        │
        ▼
OnFacingDirectionChanged
        │
        ▼
SpriteDirection
```

Avoid polling where an event naturally exists.

---

# State Machine Architecture

Player and Enemy use the same pattern.

States receive their StateMachine through the constructor.

```cpp
new IdleState(this)
```

Base state stores:

```text
StateMachine
Controller
```

Enter() performs state entry only.

Dependencies should not be injected through Enter().

---

# Animation Architecture

Gameplay never controls animations directly.

Flow:

```text
Movement
        │
        ▼
CharacterAnimation
        │
        ▼
Animator Parameters
        │
        ▼
Animator Controller
        │
        ▼
Blend Trees
        │
        ▼
Animation Clips
```

CharacterAnimation only updates:

* MoveX
* MoveY
* Speed

---

# Animator Structure

## Player

```text
Base Layer

└── Locomotion
```

## Enemy

```text
Base Layer

└── Locomotion
```

Locomotion is a **1D Blend Tree**.

```
Speed

0
↓

Idle (2D Blend Tree)

1
↓

Walk (2D Blend Tree)
```

Idle and Walk are **2D Freeform Directional Blend Trees**.

Parameters:

```text
MoveX
MoveY
```

Animation clips:

```text
IdleUp
IdleDown
IdleSide

WalkUp
WalkDown
WalkSide
```

Horizontal movement uses `SpriteRenderer.flipX`.

No Left animation clips are required.

---

# Enemy Animation Strategy

Enemy animation logic is shared.

```text
Enemy.controller
```

Each enemy uses an Animator Override Controller.

Example:

```text
Enemy.controller
        │
        ▼
Slime.overrideController
        │
        ▼
Slime Animation Clips
```

Characters with identical animation state machines should share one Animator Controller.

Only enemies with different animation logic should receive a separate controller.

---

# Rigidbody2D Defaults

Characters use:

```text
Body Type:
    Dynamic

Gravity Scale:
    0

Interpolate:
    Interpolate

Collision Detection:
    Continuous

Freeze Rotation Z:
    Enabled
```

Movement directly controls velocity.

---

# Folder Structure

```text
Assets
│
├── Art
│
├── Audio
│
├── Prefabs
│   ├── Core
│   │   └── Core.prefab
│   │
│   ├── Gameplay
│   │   ├── Player.prefab
│   │   └── Enemies
│   │       └── Slime.prefab
│   │
│   ├── Environment
│   ├── Effects
│   └── UI
│
├── Scenes
│
├── ScriptableObjects
│
├── Scripts
│   ├── Core
│   ├── Gameplay
│   │   ├── Character
│   │   │   └── Components
│   │   ├── Combat
│   │   └── World
│   │
│   ├── Player
│   ├── Enemy
│   └── UI
│
└── UI
```

---

# Naming Conventions

Gameplay systems:

```text
Movement
Health
Interaction
CharacterAnimation
SpriteDirection
```

Controllers:

```text
PlayerController
EnemyController
```

Base states:

```text
PlayerState
EnemyState
```

Concrete states:

```text
IdleState
MoveState
PatrolState
ChaseState
AttackState
DeadState
```

Namespaces provide context instead of prefixes.

---

# Coding Style

Principles:

* Single Responsibility Principle
* Small focused components
* Self-documenting code
* Explain architecture through comments

Comment conventions:

```cpp
// NOTE:
```

General explanation.

```cpp
// ARCH:
```

Architecture decisions.

```cpp
// PERF:
```

Performance considerations and future optimizations.

Performance comments should only be added where a genuine optimization concern exists.

---

# Performance Plan

The project intentionally starts with conventional Unity architecture.

Current technologies:

* MonoBehaviour
* Update()
* Rigidbody2D
* Animator

Future optimization may include:

* Object Pooling
* Event-driven updates
* Centralized Update Loop
* Data-Oriented Programming
* Unity Jobs
* Burst
* ECS-inspired processing

Optimization should always be driven by profiling.

---

# Development Roadmap

* [x] **Foundation** - The project starts and is easy to expand.

  * [x] Project folder structure
  * [x] Documentation
  * [x] Bootstrap
  * [x] Scene Management
  * [x] Input System
  * [x] Camera
  * [x] Test Scene

* [x] **First Playable Character** - Walk around an empty map.

  * [x] Player prefab
  * [x] Player Controller
  * [x] Player State Machine
  * [x] Movement
  * [x] Animation
  * [x] Interaction
  * [x] Basic UI (HP)

* [ ] **First Enemy** - An enemy can detect and chase the player.

  * [x] Enemy prefab
  * [x] Enemy State Machine
  * [ ] Idle
  * [ ] Patrol
  * [ ] Chase
  * [ ] Enemy Animation

* [ ] **First Combat Loop** - The player can kill enemies.

  * [ ] Weapon
  * [ ] Attack
  * [ ] Health Events
  * [ ] Damage
  * [ ] Death
  * [ ] Hit Effects

* [ ] **First Dungeon** - A complete playable level.

  * [ ] Tilemap
  * [ ] Collision
  * [ ] Enemy Spawners
  * [ ] Exit Portal

* [ ] **Loot Loop**

  * [ ] Item Drops
  * [ ] Item Pickup
  * [ ] Inventory
  * [ ] Equipment

* [ ] **Character Progression**

  * [ ] Experience
  * [ ] Level System
  * [ ] Character Stats
  * [ ] Skills

* [ ] **NPC & Town**

  * [ ] NPC
  * [ ] Dialogue
  * [ ] Shop
  * [ ] Stash
  * [ ] Healing

* [ ] **Quest System**

  * [ ] Quest Data
  * [ ] Quest Tracking
  * [ ] Rewards

* [ ] **Polish**

  * [ ] Audio
  * [ ] Visual Effects
  * [ ] Better UI
  * [ ] More Enemy Types
  * [ ] Boss Fight

* [ ] **Performance Optimization**

  * [ ] Object Pooling
  * [ ] Event-driven Systems
  * [ ] Reduce Allocations
  * [ ] Centralized Update Loop
  * [ ] Data-Oriented Refactoring
  * [ ] Profiling & Optimization

Each milestone should leave the project in a playable state.

---

# Preferred Assistance Style

When helping with this project:

* Explain architecture before implementation.
* Prefer conventional Unity solutions first.
* Mention future optimizations without implementing them prematurely.
* Keep Gameplay systems generic and reusable.
* Keep Player, Enemy and NPC focused on decision making.
* Explain why design decisions are made, not just how.
* Progress one feature at a time following the roadmap.
* Include meaningful `NOTE`, `ARCH` and `PERF` comments where appropriate.
* Keep components small and focused. Prefer introducing new components over expanding existing ones beyond a single responsibility.
