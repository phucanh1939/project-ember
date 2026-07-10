# Project Context Update - Enemy Architecture

## Enemy System Architecture

The current enemy architecture follows a traditional Unity OOP approach.

The runtime flow:

```
EnemyController
        |
        v
    EnemyBrain
        |
        v
 EnemyStateMachine
        |
        v
    EnemyState
```

---

## EnemyController

`EnemyController` is the main MonoBehaviour attached to enemy prefabs.

Responsibilities:

* Own references to gameplay components:

  * Movement
  * Health
  * Sensor
  * Other shared enemy components
* Provide shared data access for states.
* Initialize and connect the enemy AI system.

Example responsibilities:

```
EnemyController
    |
    + Movement
    + Health
    + Sensor
    + Brain
```

The controller does not contain enemy behavior logic.

Behavior is delegated to the Brain and State system.

---

# EnemyBrain

`EnemyBrain` defines the AI setup for a specific enemy type.

Responsibilities:

* Own the enemy AI configuration.
* Initialize the state machine.
* Create states required by that enemy type.
* Define the initial state.

Different enemy types can have different brains:

```
SlimeBrain
    |
    States:
        Idle
        Chase
        Attack


ArcherBrain
    |
    States:
        Idle
        Patrol
        Chase
        Shoot
        Retreat
```

The Brain decides which states exist.

The StateMachine only executes them.

---

# EnemyStateMachine

Responsibilities:

* Store available states.
* Keep the current active state.
* Handle state lifecycle.

It does NOT:

* Create states.
* Know enemy types.
* Decide behavior.

Example:

```
EnemyStateMachine

States:
    IdleState
    ChaseState
    AttackState

Current:
    ChaseState
```

Flow:

```
CurrentState.Exit()

        |

NewState.Enter()

        |

NewState.Update()
```

---

# EnemyState

Base class:

```csharp
EnemyState
{
    EnemyStateMachine
    EnemyController

    Enter()
    Exit()
    Update()
}
```

States receive references to:

* StateMachine
* EnemyController

This allows states to:

* Read enemy data.
* Call gameplay components.
* Request transitions.

Example:

```
ChaseState

if distance < attackRange

    StateMachine.ChangeState(Attack)
```

---

# ScriptableObject Based AI Configuration

The current direction is moving state creation into ScriptableObjects.

Goal:

Create enemy types entirely from the Unity Editor without writing a new Brain class for every enemy.

Structure:

```
EnemyBrain
      |
      v
EnemyAIConfig (ScriptableObject)
      |
      |
      + StateDefinitions[]
```

Example:

```
SlimeAIConfig

States:
    IdleStateDefinition
    ChaseStateDefinition
    AttackStateDefinition


Initial State:
    Idle
```

---

# StateDefinition

A ScriptableObject describing how to create a state.

Responsibilities:

* Store state configuration.
* Create runtime state instances.

Example:

```
ChaseStateDefinition

Create()
    |
    v
new ChaseState(
    stateMachine,
    controller
)
```

Important:

The ScriptableObject is NOT the runtime state.

It is only a factory/configuration asset.

Runtime:

```
Editor Asset

ChaseStateDefinition
        |
        |
        v

Runtime Object

ChaseState
```

---

# Enemy Creation Flow

Runtime:

```
Enemy Prefab Spawn

        |

EnemyController Awake/Initialize

        |

EnemyBrain.Initialize()

        |

Load EnemyAIConfig

        |

For each StateDefinition:

        Create State

        |

Register into StateMachine

        |

Change to Initial State

        |

Enemy starts running AI
```

---

# Current Folder Direction

Current project structure:

```
Assets

├── Prefabs
│   └── Gameplay
│       └── Enemies

├── ScriptableObjects

├── Scripts
│
├── Gameplay
│   ├── Character
│   ├── Combat
│   └── World
│
└── Enemy
    ├── EnemyController
    ├── Brain
    ├── StateMachine
    ├── States
    └── Config
```

Enemy-related configuration assets should live under:

```
Assets/ScriptableObjects/Enemy/
```

or a similar grouped configuration folder.

---

# Future Optimization Direction

The project will continue with traditional Unity architecture first.

Optimization mindset:

* Keep data close together.
* Process similar data together.
* Avoid unnecessary object traversal.
* Use Jobs/Burst where data layout allows.

Possible future evolution:

```
Current:

EnemyController
    |
    Brain
    |
    StateMachine


Future:

World Data
    |
    Systems
    |
    Presentation Sync
```

The goal is not immediately moving to DOTS, but applying data-oriented principles where useful.

---

# Current Enemy Design Goal

Build a flexible enemy framework where:

* New enemy types can be created mostly from ScriptableObject configuration.
* States are reusable.
* State logic is independent from specific enemy types.
* Enemy behavior is composed instead of hardcoded.
* Architecture can later evolve toward data-oriented processing if needed.

```

This should be enough as the starting context for the next chat.
```
