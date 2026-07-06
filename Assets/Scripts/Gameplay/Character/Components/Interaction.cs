using UnityEngine;

/// <summary>
/// Detects nearby interactable objects and triggers interactions.
///
/// ------------------------------------------------------------------
/// Responsibility
/// ------------------------------------------------------------------
///
/// - Detect nearby interactable objects.
/// - Maintain the current interaction target.
/// - Invoke interactions when requested.
///
/// This component does not know the concrete type of the interactable.
/// It only communicates through the IInteractable interface.
/// </summary>
public class Interaction : MonoBehaviour
{
    [Header("Detection")]
    [SerializeField]
    private Movement _movement;

    [SerializeField]
    private float _interactionDistance = 1.0f;

    [SerializeField]
    private LayerMask _interactionLayer;

    /// <summary>
    /// The interactable object currently in front of the character.
    /// </summary>
    public IInteractable CurrentTarget { get; private set; }

    private void Update()
    {
        DetectTarget();
    }

    /// <summary>
    /// Finds the interactable object in front of the character.
    /// </summary>
    private void DetectTarget()
    {
        RaycastHit2D hit = Physics2D.Raycast(
            transform.position,
            _movement.FacingDirection,
            _interactionDistance,
            _interactionLayer);

        if (!hit)
        {
            CurrentTarget = null;
            return;
        }

        CurrentTarget = hit.collider.GetComponent<IInteractable>();

        // NOTE:
        // If the collider does not implement IInteractable,
        // CurrentTarget becomes null.
    }

    /// <summary>
    /// Attempts to interact with the current target.
    /// </summary>
    public void TryInteract()
    {
        CurrentTarget?.Interact(gameObject);
    }

    // ARCH:
    // Interaction only depends on the IInteractable interface.
    // It never checks whether the object is an NPC, Chest, Door, etc.

    // NOTE:
    // The current target is detected continuously so UI systems can
    // display prompts or highlights before the player presses Interact.

    // PERF:
    // A raycast every frame is acceptable for most games.
    // During the optimization phase we will compare this approach
    // against trigger colliders or spatial partitioning if necessary.
}
