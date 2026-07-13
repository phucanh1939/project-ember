using Game.Core;
using UnityEngine;

namespace Game.Gameplay.Player
{
    /// <summary>
    /// PlayerInput is a thin adapter between InputService and Player gameplay logic.
    ///
    /// It does NOT read Unity Input System directly.
    /// It only reads processed input state from InputService.
    ///
    /// ROLE:
    /// InputService → PlayerInput → PlayerController → Gameplay Systems
    /// </summary>
    public class PlayerInput : MonoBehaviour
    {
        public Vector2 Move { get; private set; }

        public bool AttackPressed { get; private set; }
        public bool InteractPressed { get; private set; }
        public bool DashPressed { get; private set; }

        private InputService input;

        public void Initialize(InputService inputService)
        {
            input = inputService;
        }

        private void Update()
        {
            // Read continuous input
            Move = input.Move;

            // Read one-frame actions
            AttackPressed = input.AttackPressed;
            InteractPressed = input.InteractPressed;
            DashPressed = input.DashPressed;

            // IMPORTANT:
            // Consume one-frame inputs so they are not reused
            input.ConsumeInputs();

            if (AttackPressed) Debug.Log("------- AttackPressed: " + AttackPressed);

        }
    }
}