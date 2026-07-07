using UnityEngine;

namespace Game.Gameplay
{
    /// <summary>
    /// Updates the character's Animator based on gameplay state.
    ///
    /// ------------------------------------------------------------------
    /// Required Unity Setup
    /// ------------------------------------------------------------------
    ///
    /// Animator Parameters:
    ///
    ///     MoveX   (Float)
    ///     MoveY   (Float)
    ///     Speed   (Float)
    ///
    /// Expected Animator Layout:
    ///
    ///                 Any State
    ///                     │
    ///                     ▼
    ///               Blend Tree (Locomotion)
    ///
    /// The Blend Tree should use:
    ///
    ///     Parameter:
    ///         Speed
    ///
    ///     Threshold:
    ///         0 = Idle
    ///         1 = Walking
    ///
    /// Inside each motion (Idle / Walk), use a 2D Freeform Directional
    /// Blend Tree driven by:
    ///
    ///     MoveX
    ///     MoveY
    ///
    /// This allows the character to:
    ///
    /// - Face Up
    /// - Face Down
    /// - Face Left
    /// - Face Right
    /// - Transition smoothly while moving.
    ///
    /// ------------------------------------------------------------------
    /// Responsibility
    /// ------------------------------------------------------------------
    ///
    /// This component observes gameplay components such as Movement.
    /// It never changes gameplay itself.
    /// </summary>
    [RequireComponent(typeof(Animator))]
    public class CharacterAnimation : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private Movement _movement;

        private static readonly int MoveXHash = Animator.StringToHash("MoveX");
        private static readonly int MoveYHash = Animator.StringToHash("MoveY");
        private static readonly int SpeedHash = Animator.StringToHash("Speed");

        private void OnValidate()
        {
            _animator = GetComponent<Animator>();
        }

        private void Update()
        {
            Vector2 velocity = _movement.Velocity;
            Vector2 facing = _movement.FacingDirection;

            _animator.SetFloat(MoveXHash, facing.x);
            _animator.SetFloat(MoveYHash, facing.y);
            _animator.SetFloat(SpeedHash, velocity.sqrMagnitude);
        }

        // ARCH:
        // CharacterAnimation observes Movement.
        // It does not receive animation commands from PlayerController.
        //
        // This keeps gameplay independent from presentation.

        // PERF:
        // Every character currently updates its Animator every frame.
        // Later we'll investigate whether parameter updates can be
        // skipped when values haven't changed.
    }
}