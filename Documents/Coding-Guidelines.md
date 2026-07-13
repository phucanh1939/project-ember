# Coding Guidelines

## Purpose

These guidelines define the coding style and architecture rules used in the project.

Goals:

* Consistent codebase
* Clear responsibilities
* Easy maintenance
* Easy optimization later

Follow these rules unless there is a strong reason not to.

---

# General Principles

* Prefer readability over clever solutions.
* Keep classes focused on one responsibility.
* Prefer composition over inheritance.
* Keep dependencies explicit.
* Avoid premature optimization.
* Write code for future maintainers.

---

# Naming

## Classes

Use PascalCase.

```csharp
PlayerController
CharacterAnimation
Movement
```

## Interfaces

Prefix with `I`.

```csharp
IDamageable
IInteractable
```

## Methods

Use PascalCase and describe the action.

```csharp
Move()
TakeDamage()
SetMoveDirection()
```

## Private Fields

Use camelCase with `_` prefix.

```csharp
private Movement _movement;
private Rigidbody2D _rigidbody;
```

## Properties

Use PascalCase.

```csharp
public Vector2 Velocity { get; }
public bool IsAlive { get; }
```

## Constants

Use PascalCase.

```csharp
private const float MoveSpeed = 5f;
```

---

# Serialization

Prefer serialized private fields.

Good:

```csharp
[SerializeField]
private Movement _movement;
```

Avoid:

```csharp
public Movement movement;
```

Expose data through read-only properties when needed.

```csharp
public Vector2 Velocity => _velocity;
```

---

# Field Formatting

Keep simple fields on one line.

Good:

```csharp
[SerializeField] private Hitbox _hitbox;
```

Avoid:
```csharp
[SerializeField]
private Hitbox _hitbox;
```

# Class Responsibility

Each class should have one clear purpose.

Examples:

```text
PlayerInput
    Reads input

Movement
    Handles movement

Health
    Stores health state

CharacterAnimation
    Updates Animator
```

Avoid classes that control unrelated systems.

---

# MonoBehaviour Lifecycle

Use Unity callbacks consistently.

## OnValidate()

* Assign references.
* Validate setup.

Example:

```csharp
_movement = GetComponent<Movement>();
```

## Awake()

* Initialize internal state.
* Cache references.
* Create helper objects.

## OnEnable()

* Subscribe to events.

## Start()

* Initialization requiring other objects.

## Update()

Frame-based gameplay logic.

Examples:

* State machines
* Input checks
* Timers

## FixedUpdate()

Physics only.

Examples:

* Rigidbody movement
* Forces

## LateUpdate()

Post-update logic.

Examples:

* Camera follow
* One-frame cleanup

## OnDisable()

* Unsubscribe from events.

---

# Dependencies

Dependencies should be visible.

Good:

```csharp
[SerializeField]
private Movement _movement;
```

Avoid hidden lookups:

```csharp
FindObjectOfType()
GameObject.Find()
FindFirstObjectByType()
```

Cache references instead of repeatedly calling:

```csharp
GetComponent()
```

---

# Comments

Comments should explain intent, not describe code.

Use:

```csharp
// NOTE:
// Important implementation detail.

// ARCH:
// Architecture decision.

// PERF:
// Performance consideration.

// TODO:
// Future work.
```

Avoid:

```csharp
// Set speed
_speed = 5;
```

---

# Methods

Keep methods small and focused.

Good:

```csharp
ReadInput()
Move()
Attack()
```

Avoid large methods handling multiple responsibilities.

---

# State Machines

State behavior belongs inside states.

Avoid:

```csharp
if (isDead)
if (isAttacking)
if (isDashing)
```

spread throughout the code.

Prefer:

```text
StateMachine
      |
      CurrentState
```

---

# Gameplay vs Presentation

Gameplay should not directly control visuals.

Good:

```text
Movement
    |
    Velocity

CharacterAnimation
    |
    Animator
```

Animation observes gameplay state.

---

# Error Handling

Fail early during development.

Validate required references:

```csharp
Debug.Assert(_movement != null);
```

Avoid silently ignoring missing dependencies.

---

# Performance

Prioritize clean architecture first.

Current rules:

* Cache references.
* Avoid unnecessary allocations.
* Avoid hidden lookups.
* Measure before optimizing.

Future optimization may introduce:

* Object pooling
* Centralized update systems
* Data-oriented design
* Burst / Jobs

---

# Code Organization

Order members consistently:

```csharp
Fields

Properties

Unity Callbacks

Public Methods

Private Methods
```

---

# Documentation

Gameplay components should include a short XML summary.

Example:

```csharp
/// <summary>
/// Handles character movement using Rigidbody2D.
/// </summary>
```

Add architecture or performance notes only when they provide useful context.

---

# Commit Checklist

Before committing:

* Does this class have one responsibility?
* Are dependencies explicit?
* Is the API smaller than necessary?
* Are comments explaining intent?
* Can another developer understand this quickly?

---

# Summary

Keep code simple, explicit, and focused.

Clear responsibilities and consistent structure are more important than clever implementations.
