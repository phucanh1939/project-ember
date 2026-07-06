# Unity Input System

## Core Concepts

- **Binding** = physical input (WASD, mouse, gamepad stick)
- **Action** = gameplay intent (Move, Attack, Interact)

- **Action Type**
  - Button → on/off input (Attack, Jump)
  - Value → continuous input (Move, Aim)

- **Control Type**
  - Vector2 → movement / direction
  - float → analog value (trigger, slider)

---

## Action Map

- **Action Map = input context group**
- A way to switch between different control sets depending on game state

### Examples

- Gameplay map:
  - Move
  - Attack
  - Dash
  - Interact

- UI map:
  - Navigate
  - Submit
  - Cancel

---

## Why Action Maps matter

They allow enabling/disabling input contexts:

- In gameplay → Gameplay map active
- In menu → UI map active
- In dialogue → both disabled or limited input

---

## Flow

Device → Binding → Action → Action Map → Action Type → Control Type → Value

---

## Example

Move:
- Action Map: Gameplay
- Binding: WASD + Left Stick
- Action Type: Value
- Control Type: Vector2
→ Output: movement direction