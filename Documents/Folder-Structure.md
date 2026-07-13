# Folder Structure

## Philosophy

The project is organized by **feature** and **domain** rather than by script type.

Instead of:

```text
Scripts
├── Components
├── Controllers
├── Managers
└── Utilities
```

we prefer:

```text
Scripts
├── Gameplay
├── Player
├── Enemy
├── NPC
└── Core
```

This keeps related code together and allows the project to grow naturally without constant reorganization.

When deciding where a new file belongs, ask:

> **Which feature owns this file?**

If the file can be reused by multiple features (for example, both Player and Enemy), it belongs in a shared gameplay module.

---

# Project Structure

```text
Assets
│
│── Animations
├── Art
│   ├── Materials
│   ├── Sprites
│   └── Tiles
|   └── Fonts
│
├── Audio
│   ├── Music
│   ├── SFX
│   └── Mixer
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
```

---

# Organization Rules

- Organize by feature, not script type.
- Keep related files together.
- Put reusable gameplay systems in `Scripts/Gameplay`.
- Put feature-specific logic in its feature folder (`Player`, `Enemy`, `NPC`, etc.).
- Keep runtime code separate from assets.
- Design the folder structure so it can grow without major reorganization.