using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Core
{
    /// <summary>
    /// InputService is the single entry point for all player input in the game.
    ///
    /// It wraps Unity's Input System and exposes a simplified, gameplay-friendly API.
    ///
    /// ARCHITECTURE ROLE:
    /// Input System (Unity) → InputService → PlayerInput / Gameplay Systems
    ///
    /// RESPONSIBILITIES:
    /// - Read raw input from Unity Input System (GameInputActions)
    /// - Convert input into simple gameplay state (Move, Attack, Interact, Dash)
    /// - Provide global access via singleton
    /// - Store input state for one-frame actions
    ///
    /// LIFECYCLE:
    /// This service does NOT manage its own persistence.
    /// It is expected to be placed under a persistent Core object
    /// (e.g. CoreRoot with DontDestroyOnLoad).
    ///
    /// NOTE:
    /// Input maps switching (Gameplay / UI / Cutscene) will be added later
    /// without changing gameplay code.
    /// </summary>
    public class InputService : MonoBehaviour
    {
        public static InputService Instance { get; private set; }

        private GameInputActions input;

        public Vector2 Move { get; private set; }

        public bool AttackPressed { get; private set; }
        public bool InteractPressed { get; private set; }
        public bool DashPressed { get; private set; }

        private void Awake()
        {
            Instance = this;
            input = new GameInputActions();
        }

        private void OnEnable()
        {
            input.Enable();

            input.Player.Move.performed += OnMove;
            input.Player.Move.canceled += OnMove;

            input.Player.Attack.performed += _ => AttackPressed = true;
            input.Player.Interact.performed += _ => InteractPressed = true;
            input.Player.Dash.performed += _ => DashPressed = true;
        }

        private void OnDisable()
        {
            input.Disable();
        }

        private void OnMove(InputAction.CallbackContext ctx)
        {
            Move = ctx.ReadValue<Vector2>();
        }

        /// <summary>
        /// Clears one-frame input flags after they are consumed by gameplay systems.
        /// </summary>
        public void ConsumeInputs()
        {
            AttackPressed = false;
            InteractPressed = false;
            DashPressed = false;
        }
    }
}