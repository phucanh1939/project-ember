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

```
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
* Utilities

Core never contains gameplay logic.

---

## Gameplay

Shared gameplay systems.

Gameplay systems define **how mechanics work**, not **who uses them**.

Examples:

* Movement
* Combat
* Interaction
* Health
* Future Gameplay systems

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

It does NOT implement movement or combat.

Instead it uses Gameplay systems.

---

## Enemy

Enemy follows the same architecture as Player.

Enemy AI decides behavior.

Gameplay systems execute actions.

---

# Player Architecture

Player uses a State-Driven architecture.

```
PlayerInput
      ↓
PlayerController
      ↓
PlayerStateMachine
      ↓
Current State
      ↓
Gameplay Systems
```

PlayerController is intentionally thin.

The StateMachine decides behavior.

Gameplay systems execute behavior.

---

# Gameplay Component Philosophy

Components such as:

* Movement
* Combat
* Interaction

are **Gameplay systems**, not Player components.

They belong under:

```
Scripts/Gameplay/
```

Player, Enemy and NPC all reuse them.

---

# Current Gameplay Components

Implemented or designed:

* Movement
* Character Animation
* Player Input
* Interaction
* Player Controller
* Player State Machine

Movement owns:

* Rigidbody2D movement
* Velocity
* Facing Direction

Movement never reads input.

CharacterAnimation observes Movement.

PlayerController reads PlayerInput.

PlayerStateMachine controls behavior.

---

# Folder Structure

```text
Assets
│
├── Art
│   ├── Animations
│   ├── Materials
│   ├── Sprites
│   └── Tiles
│
├── Audio
│   ├── Music
│   ├── SFX
│   └── Mixer
│
├── Documentation
│   ├── 00-Architecture
│   ├── 01-Core
│   ├── 02-Player
│   ├── 03-Combat
│   └── 99-Optimization
│
├── Editor
│
├── Plugins
│
├── Prefabs
│   ├── Characters
│   ├── Environment
│   ├── Items
│   ├── Effects
│   └── UI
│
├── Resources
│
├── Scenes
│
├── ScriptableObjects
│   ├── Characters
│   ├── Items
│   ├── Skills
│   └── Dialogue
│
├── Scripts
│   ├── Core
│   ├── Gameplay
│   │   ├── Characters
│   │   ├── Combat
│   │   ├── Dialogue
│   │   ├── Inventory
│   │   └── World
│   │
│   ├── Player
│   ├── Enemy
│   ├── NPC
│   └── UI
│
├── Settings
│
├── StreamingAssets
│
├── Tests
│   ├── EditMode
│   └── PlayMode
│
├── ThirdParty
│
└── UI
    ├── Fonts
    ├── Icons
    ├── Prefabs
    └── Sprites
```

---

# Coding Style

Preferred style:

* Single Responsibility Principle
* Small components
* Self-documenting code
* Extensive comments explaining architectural decisions
* Performance notes marked with:

```cpp
// PERF:
```

Architecture notes marked with:

```cpp
// ARCH:
```

General explanations marked with:

```cpp
// NOTE:
```

Documentation is as important as implementation.

Every major system should have its own design document.

---

# Performance Plan

The project intentionally starts with conventional Unity development.

Examples:

* MonoBehaviour
* Update()
* Rigidbody2D
* Animator

Later, after gameplay is complete, systems will be profiled and gradually refactored.

Potential optimizations include:

* Object Pooling
* Event-driven updates
* Centralized update loop
* Data-Oriented Programming
* Unity Jobs
* Burst
* ECS-inspired processing where appropriate

Optimization should always be driven by profiling.

---

# Development Roadmap

- [ ] **Foundation** - The project starts and is easy to expand.
  - [x] Project folder structure
  - [x] Documentation
  - [x] Bootstrap
  - [x] Scene Management
  - [ ] Input System
  - [ ] Camera
  - [ ] Test Scene

- [ ] **First Playable Character** - Walk around an empty map.
  - [ ] Player prefab
  - [ ] Player Controller
  - [ ] Player State Machine
  - [ ] Movement
  - [ ] Animation
  - [ ] Interaction
  - [ ] Basic UI (HP)

- [ ] **First Enemy** - An enemy can detect and chase the player.
  - [ ] Enemy prefab
  - [ ] Enemy State Machine
  - [ ] Idle
  - [ ] Patrol
  - [ ] Chase
  - [ ] Enemy Animation

- [ ] **First Combat Loop** - The player can kill enemies.
  - [ ] Weapon
  - [ ] Attack
  - [ ] Health
  - [ ] Damage
  - [ ] Death
  - [ ] Hit Effects

- [ ] **First Dungeon** - A complete playable level.
  - [ ] Tilemap
  - [ ] Collision
  - [ ] Enemy Spawners
  - [ ] Exit Portal

- [ ] **Loot Loop** - Defeat enemies to become stronger.
  - [ ] Item Drops
  - [ ] Item Pickup
  - [ ] Inventory
  - [ ] Equipment

- [ ] **Character Progression** - Grow stronger over time.
  - [ ] Experience
  - [ ] Level System
  - [ ] Character Stats
  - [ ] Skills

- [ ] **NPC & Town** - A safe hub for the player.
  - [ ] NPC
  - [ ] Dialogue
  - [ ] Shop
  - [ ] Stash
  - [ ] Healing

- [ ] **Quest System** - Give the player objectives.
  - [ ] Quest Data
  - [ ] Quest Tracking
  - [ ] Rewards

- [ ] **Polish** - Improve the overall experience.
  - [ ] Audio
  - [ ] Visual Effects
  - [ ] Better UI
  - [ ] More Enemy Types
  - [ ] Boss Fight

- [ ] **Performance Optimization** - Improve scalability and performance.
  - [ ] Object Pooling
  - [ ] Event-driven Systems
  - [ ] Reduce Allocations
  - [ ] Centralized Update Loop
  - [ ] Data-Oriented Refactoring
  - [ ] Profiling & Optimization

Each milestone should leave the project in a playable state.

---

# Preferred Assistance Style

When helping with this project:

* Explain architecture before implementation.
* Prefer conventional Unity solutions first.
* Mention where future optimizations could be applied, but do not implement them yet.
* Keep Gameplay systems generic and reusable.
* Keep Player, Enemy and NPC focused on decision making.
* Explain why design decisions are made, not just how.
* Progress one feature at a time following the roadmap.
* When writing code, include meaningful comments (`NOTE`, `ARCH`, `PERF`) to explain design choices and future optimization opportunities.
