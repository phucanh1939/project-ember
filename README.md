# Project Ember

> A 2D top-down Action RPG built in Unity, inspired by the gameplay loop of classic ARPGs such as Diablo II.

# Overview

Project Ember is a learning project focused on building a complete, modular, and maintainable Action RPG from the ground up.

The project emphasizes **clean architecture**, **reusable gameplay systems**, and **incremental development**. Every feature is designed using conventional Unity practices first, then later evaluated for performance improvements using data-oriented techniques.

The goal is not only to create a fun game, but also to build a codebase that is easy to understand, extend, and optimize.

# Objectives

* Build a complete 2D top-down Action RPG.
* Learn and apply good software architecture.
* Separate gameplay decisions from gameplay execution.
* Create reusable gameplay systems shared by Player, Enemy, and NPC.
* Gradually refactor the project toward a data-oriented architecture after the game is fully playable.

# Core Gameplay

The player explores handcrafted maps, defeats monsters, collects equipment, grows stronger, and completes quests while progressing through increasingly difficult areas.

The gameplay is inspired by the classic Action RPG loop:

```
Explore
    ↓
Fight Enemies
    ↓
Collect Loot
    ↓
Upgrade Character
    ↓
Explore Stronger Areas
```

# Architecture Goals

The project follows several architectural principles:

* Small, focused components
* Clear separation of responsibilities
* Shared Gameplay systems
* State-driven behavior
* Feature-oriented organization
* Performance considered after functionality

# Development Philosophy

The project is developed in small, playable milestones.

Each milestone introduces a complete gameplay feature before moving on to the next one. Once all core features are implemented, the project will be profiled and optimized using data-oriented techniques where they provide measurable benefits.

# License

This project is intended for learning, experimentation, and portfolio development.
