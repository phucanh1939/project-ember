# Project Architecture

## Introduction

Before writing gameplay code, it's important to define **how the project is organized**.

As games grow, the biggest challenge is rarely implementing new features—it is keeping the codebase understandable and maintainable. Good architecture helps us build new features without constantly modifying existing ones.

The goal of this project is to build a 2D Top-Down RPG using a clean, component-based architecture that starts with conventional Unity practices and gradually evolves toward more data-oriented designs.

This documentation describes the architectural principles followed throughout the project.

---

# Design Goals

The architecture is designed around the following goals:

- Keep each class focused on a single responsibility.
- Favor composition over inheritance.
- Minimize coupling between systems.
- Allow gameplay components to be reused.
- Separate gameplay from presentation.
- Make future performance optimizations straightforward.
- Keep the code easy to understand for future developers.

---

# Core Philosophy

The project follows a simple philosophy:

> **Controllers make decisions. Components perform actions.**

This single rule influences almost every architectural decision.

For example:

```
PlayerInput

↓

PlayerController

↓

Movement
```

The `PlayerController` decides **where** the player should move.

The `Movement` component decides **how** to move the character.

---

# Component-Based Architecture

Instead of placing every feature inside one large Player script, the player is built by composing small, specialized components.

```
Player

├── PlayerInput
├── PlayerController
├── PlayerStateMachine
├── Movement
├── CharacterAnimation
└── Interaction
```

Each component has a clearly defined responsibility.

This keeps systems independent and reusable.

---

# Single Responsibility Principle

Every component should have **one reason to change**.

For example:

| Component | Responsibility |
|-----------|----------------|
| PlayerInput | Read player input |
| PlayerController | Coordinate gameplay |
| Movement | Move the Rigidbody |
| CharacterAnimation | Update Animator parameters |
| Interaction | Interact with nearby objects |

Avoid components that try to perform several unrelated tasks.

---

# Composition over Inheritance

Gameplay features are added by attaching components instead of creating deep inheritance hierarchies.

Avoid designs such as:

```
Character

↓

Player

↓

Mage

↓

FireMage

↓

EliteFireMage
```

Instead:

```
Player

├── Movement
├── Combat
├── Inventory
├── Skills
└── Interaction
```

Composition keeps the project flexible and easier to extend.

---

# Controllers

Controllers coordinate gameplay.

Examples:

```
PlayerController

EnemyController

NPCController
```

Controllers should:

- Read input or AI decisions.
- Coordinate components.
- Trigger state transitions.

Controllers should **not** implement gameplay systems directly.

---

# Components

Components perform gameplay actions.

Examples:

```
Movement

Combat

Health

Inventory

Interaction
```

Components should not decide **when** they are used.

They simply perform the requested work.

---

# State Machines

As gameplay becomes more complex, different actions become valid only in certain situations.

For example:

- The player should not move while dead.
- The player may be unable to attack while dashing.
- The player cannot interact during a cutscene.

Rather than scattering these rules throughout the project, the architecture centralizes them inside a state machine.

```
PlayerController

↓

PlayerStateMachine

↓

Idle

Move

Attack

Dash

Dead
```

Each state defines the behavior that is valid while it is active.

---

# Gameplay vs Presentation

Gameplay and presentation should remain separate.

Gameplay:

```
Movement

↓

Velocity
```

Presentation:

```
CharacterAnimation

↓

Animator

↓

Animation Clips
```

Gameplay never tells the Animator which clip to play.

Instead, gameplay exposes its current state, and the animation system presents it visually.

---

# One-Way Data Flow

Gameplay follows a one-way flow of information.

```
Player Input

↓

Gameplay Decision

↓

Gameplay Components

↓

Gameplay State

↓

Animation

↓

Rendering
```

Information should always move in one direction.

Avoid circular dependencies.

---

# Dependency Rules

Dependencies should always point toward reusable systems.

Good:

```
PlayerController

↓

Movement
```

Good:

```
EnemyController

↓

Movement
```

Bad:

```
Movement

↓

PlayerController
```

Reusable components should never depend on specific gameplay controllers.

---

# Shared Character Components

Many gameplay systems are shared by every character.

Examples:

```
Movement

Health

Combat

CharacterAnimation

Interaction
```

These belong to the shared Gameplay/Characters module rather than the Player module.

Player, Enemy, and NPC controllers simply coordinate them.

---

# Project Layers

The project is organized into logical layers.

```
Input

↓

Controllers

↓

Gameplay Components

↓

Presentation
```

Each layer has a clear responsibility.

Higher layers may depend on lower layers.

Lower layers should never depend on higher layers.

---

# Communication Between Components

Whenever possible, communication should be explicit.

Good:

```
PlayerController

↓

Movement.SetMoveDirection()
```

Avoid:

```
Movement

↓

FindObjectOfType<PlayerController>()
```

or

```
Movement

↓

GetComponent<PlayerController>()
```

to retrieve gameplay decisions.

Dependencies should be visible and intentional.

---

# Reusability

Whenever writing a new component, ask:

> **Could this component be attached to an enemy without changing its code?**

If the answer is yes, it probably belongs in the shared Gameplay/Characters module.

Examples:

- Movement
- Health
- CharacterAnimation
- Combat
- Interaction

---

# MonoBehaviour Usage

During the early stages of development, the project follows conventional Unity practices.

Each gameplay component is implemented as a MonoBehaviour.

Example:

```
Player

├── PlayerController
├── Movement
├── CharacterAnimation
└── Interaction
```

This keeps the architecture easy to understand and aligns with standard Unity workflows.

---

# Preparing for Optimization

Although the initial implementation uses MonoBehaviours, the architecture is intentionally designed so systems can later evolve into centralized update loops.

For example:

Current:

```
Movement.Update()

Movement.Update()

Movement.Update()
```

Future:

```
MovementSystem

├── Character A
├── Character B
└── Character C
```

Because gameplay responsibilities are already separated, this evolution requires minimal changes to gameplay code.

---

# Documentation Philosophy

Every gameplay component in this project should have its own design document.

Each document should explain:

- Why the component exists.
- What it is responsible for.
- What it is not responsible for.
- How it fits into the architecture.
- How it may evolve in future optimization phases.

The goal is not only to document the code, but also to document the reasoning behind its design.

---

# Guiding Principles

When designing a new gameplay system, ask the following questions:

1. Does this class have a single responsibility?
2. Should this be a controller or a component?
3. Can this component be reused?
4. Does gameplay remain independent from presentation?
5. Are dependencies flowing in one direction?
6. Will this design still make sense after future optimization?

If the answer to any of these questions is "no", reconsider the design before implementing it.

---

# Summary

This project follows a component-based architecture built on a few simple principles:

- Build gameplay through composition.
- Controllers coordinate gameplay.
- Components perform actions.
- State machines manage gameplay rules.
- Gameplay drives presentation.
- Dependencies flow in one direction.
- Shared systems remain reusable.
- Architecture should prepare for future optimization rather than resist it.

By following these principles consistently, the project remains maintainable as new features are added and provides a strong foundation for later transitions to more performance-oriented architectures such as DOP and ECS.