# Coding Guidelines

## Purpose

This document defines the coding conventions used throughout the project.

The goal is to make the codebase:

- Consistent
- Readable
- Maintainable
- Easy to review
- Easy to optimize later

Every new class should follow these guidelines unless there is a good reason not to.

---

# General Principles

When writing code, always keep these principles in mind.

- Prefer readability over cleverness.
- Keep classes focused on a single responsibility.
- Favor composition over inheritance.
- Minimize coupling between systems.
- Optimize only after measuring performance.
- Write code for future maintainers.

---

# Naming Conventions

## Classes

Use **PascalCase**.

```csharp
PlayerController

Movement

CharacterAnimation
```

---

## Interfaces

Prefix with `I`.

```csharp
IInteractable

IDamageable

IHealable
```

---

## Methods

Use PascalCase.

```csharp
Move()

Attack()

TakeDamage()

SetMoveDirection()
```

Methods should describe **what** they do.

---

## Variables

Use camelCase with a leading underscore for private fields.

```csharp
private Movement _movement;

private Rigidbody2D _rigidbody;
```

---

## Properties

Use PascalCase.

```csharp
public Vector2 Velocity { get; }

public bool IsAlive { get; }
```

---

## Constants

Use PascalCase.

```csharp
private const float MoveSpeed = 5f;
```

---

# Serialization

Serialize private fields instead of exposing public fields.

Good:

```csharp
[SerializeField]
private Movement _movement;
```

Avoid:

```csharp
public Movement movement;
```

Use properties when other systems need read-only access.

```csharp
public Vector2 Velocity => _velocity;
```

---

# Class Responsibilities

Each class should have a single responsibility.

Example:

PlayerInput

- Read input.

PlayerController

- Coordinate gameplay.

Movement

- Move the Rigidbody.

CharacterAnimation

- Update the Animator.

Avoid classes that perform unrelated tasks.

---

# MonoBehaviour Lifecycle

Use Unity callbacks consistently.

## OnValidate()

Initialize internal references (if possible).
- GetComponent()

## Awake()

Initialize internal references.

Examples:

- GetComponent()
- Cache references
- Create helper objects

---

## OnEnable()

Subscribe to events.

---

## Start()

Initialization that depends on other objects already existing.

---

## Update()

Use for frame-based gameplay logic.

Examples:

- State machines
- Input processing
- Timers

---

## FixedUpdate()

Physics only.

Examples:

- Rigidbody movement
- Physics forces

---

## LateUpdate()

Run after Update.

Examples:

- Reset one-frame input
- Camera follow
- Post-processing

---

## OnDisable()

Unsubscribe from events.

---

## OnDestroy()

Clean up unmanaged resources if necessary.

---

# Inspector Usage

Expose only what designers need.

Avoid serializing internal implementation details.

Example:

```csharp
[SerializeField]
private float _moveSpeed = 5f;
```

Hide implementation details behind properties or methods.

---

# Component References

Cache references once.

Good:

```csharp
private void Awake()
{
    _rigidbody = GetComponent<Rigidbody2D>();
}
```

Avoid repeated GetComponent calls.

---

# Dependencies

Dependencies should be explicit.

Good:

```csharp
[SerializeField]
private Movement _movement;
```

Avoid:

```csharp
FindObjectOfType()

GameObject.Find()

FindFirstObjectByType()
```

These create hidden dependencies.

---

# Comments

Use comments to explain intent, not obvious code.

Preferred comment types:

```csharp
// NOTE:
```

Explains important implementation details.

```csharp
// ARCH:
```

Explains architectural decisions.

```csharp
// PERF:
```

Highlights current performance characteristics or future optimizations.

```csharp
// TODO:
```

Marks planned work.

Avoid comments that simply repeat the code.

Bad:

```csharp
// Increment i
i++;
```

---

# Methods

Keep methods short.

Each method should perform one task.

Good:

```csharp
ReadInput()

Move()

Attack()
```

Avoid very large methods with multiple responsibilities.

---

# Update Loops

Avoid putting unrelated logic inside one Update().

Instead of:

```text
Update()

↓

Movement

Combat

Inventory

Dialogue

Animation
```

Prefer:

```text
Controller

↓

Specialized Components
```

---

# State Machines

State-specific behavior belongs inside states.

Avoid:

```text
if (isDead)

if (isAttacking)

if (isDashing)
```

throughout the project.

Use:

```
PlayerStateMachine

↓

Current State
```

---

# Gameplay vs Presentation

Gameplay should never control presentation directly.

Good:

```
Movement

↓

Velocity
```

```
CharacterAnimation

↓

Animator
```

Animation observes gameplay.

---

# Error Handling

Fail early.

Validate required references during development.

Example:

```csharp
Debug.Assert(_movement != null);
```

Do not silently ignore missing dependencies.

---

# Performance Guidelines

Write clean code first.

Optimize only after identifying a real bottleneck.

Current project philosophy:

- Cache references.
- Avoid unnecessary allocations.
- Avoid hidden object lookups.
- Prefer simple code over micro-optimizations.

Future optimization phases will introduce:

- Object pooling
- Centralized update loops
- Data-Oriented Programming
- Burst
- Jobs

The current architecture is intentionally designed to support these improvements.

---

# Code Organization

Arrange members consistently.

Recommended order:

```csharp
Fields

Properties

Unity Callbacks

Public Methods

Private Methods
```

This makes classes easier to navigate.

---

# Documentation

Every gameplay component should include:

- XML summary
- Responsibility
- Architecture notes
- Performance notes where appropriate

Example:

```csharp
/// <summary>
/// Responsible for moving a character using Rigidbody2D.
/// </summary>
```

---

# Checklist

Before committing new code, ask:

- Does this class have one responsibility?
- Are dependencies explicit?
- Is the class reusable?
- Am I exposing only what is necessary?
- Are comments explaining intent instead of implementation?
- Would another developer understand this class quickly?

If the answer to any of these questions is "no", consider refactoring before moving on.

---

# Summary

These guidelines are intended to keep the project consistent from beginning to end.

By following common naming conventions, clear responsibilities, explicit dependencies, and consistent coding practices, we build a codebase that is easy to understand today and easy to optimize tomorrow.