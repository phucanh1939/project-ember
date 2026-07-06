# Project Context

## Project Overview
This repository contains a Unity 2D top-down RPG project built primarily as a showcase for clean architecture, scalable project structure, and performance-oriented engineering practices.

## Project Goals
- Build a maintainable and scalable Unity project from the start
- Follow clear separation of concerns and modular architecture
- Demonstrate strong object-oriented design principles
- Progressively explore performance optimization techniques later in the roadmap

## Technical Direction
The project is being developed in stages:
1. Establish a clean Unity project foundation
2. Create modular gameplay systems with clear responsibilities
3. Refine architecture using OOP and maintainable patterns
4. Later explore performance-focused approaches such as Burst Compiler and ECS

## Architecture Notes
The project is organized around a layered structure:
- Core: engine-level systems such as bootstrap, scene flow, input, camera, audio, save/load, and utilities
- Gameplay: reusable gameplay mechanics such as movement, interaction, and animation-related systems
- Player: player-specific logic, input handling, and state machine behavior

## Repository Structure
- Document/: architecture notes and project documentation
- Scripts/: implementation files for gameplay and player systems

## Development Principles
- Keep systems modular and reusable
- Prefer clear responsibility boundaries between layers
- Favor clean, readable, and maintainable code
- Design for future scalability and performance tuning

## Important Context for AI Assistants
When working on this project, assume the following:
- The project is an architectural showcase as well as a game prototype
- The codebase should remain organized and easy to extend
- Changes should respect the existing layering and dependency boundaries
- Performance optimization should be considered gradually, not as an early premature concern
- Unity-specific conventions and project structure should be respected

## Suggested Working Style
When making changes or adding features:
- Read the documentation under Document/ before implementing major changes
- Keep new systems aligned with the existing architecture
- Prefer small, focused, and well-documented changes
- Avoid introducing unnecessary complexity early

## Short Summary
This is a Unity 2D top-down RPG project focused on architectural clarity, code organization, and future performance optimization.
