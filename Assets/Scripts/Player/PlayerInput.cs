using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Reads player input from Unity's Input System.
///
/// Assumes the following Unity Input System setup:
///
/// Action Map:
///     Player
///
/// Actions:
///     Move        (Value / Vector2)
///     Attack      (Button)
///     Interact    (Button)
///     Dash        (Button)
///
/// This component only translates Unity input into gameplay commands.
/// It does not execute gameplay logic.
/// </summary>
public class PlayerInput : MonoBehaviour
{
    public Vector2 Move { get; private set; }

    public bool AttackPressed { get; private set; }

    public bool InteractPressed { get; private set; }

    public bool DashPressed { get; private set; }

    public void OnMove(InputAction.CallbackContext context)
    {
        // NOTE:
        // The Move action is configured as a Value (Vector2),
        // so Unity provides the current movement direction.
        Move = context.ReadValue<Vector2>();
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        // NOTE:
        // This callback is invoked by Unity when the player performs the
        // Attack action (e.g. presses the assigned keyboard key, gamepad
        // button, or other bound input).
        //
        // When the action enters the "Performed" phase, we record the button
        // press as a one-frame event. The value remains valid until LateUpdate(),
        // where it is reset.
        //
        // Timeline:
        //
        // Player presses Attack key/button
        //              │
        //              ▼
        //      Unity Input System
        //              │
        //              ▼
        //         OnAttack() // this function is called
        //              │
        //              ▼
        //   AttackPressed = true
        //              │
        //              ▼
        //   PlayerController.Update() // Use AttackPressed to trigger attack logic
        //              │
        //              ▼
        //       LateUpdate()
        //              │
        //              ▼
        //   AttackPressed = false
        //
        // This guarantees that the input can only be consumed during
        // the current frame.
        AttackPressed = context.performed;
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        InteractPressed = context.performed;
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        DashPressed = context.performed;
    }

    private void LateUpdate()
    {
        // NOTE:
        // Button actions are treated as one-frame events.
        // Reset them after every frame so other systems can
        // react to them exactly once.
        AttackPressed = false;
        InteractPressed = false;
        DashPressed = false;
    }

    // ARCH:
    // This component intentionally contains no gameplay logic.
    // It simply translates hardware input into data that other
    // gameplay systems can consume.

    // PERF:
    // Unity's generated Input System callbacks are perfectly
    // suitable for most games. During the optimization phase
    // we'll evaluate whether a custom input layer offers any
    // measurable benefit.
}