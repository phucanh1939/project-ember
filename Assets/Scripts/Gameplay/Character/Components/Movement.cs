using UnityEngine;

namespace Game.Gameplay
{


    /// <summary>
    /// Responsible for moving a character using Rigidbody2D.
    ///
    /// This component only executes movement commands.
    /// It does not decide where the character should move.
    /// </summary>
    [RequireComponent(typeof(Rigidbody2D))]
    public class Movement : MonoBehaviour
    {
        [Header("Movement")]
        [SerializeField]
        private float _moveSpeed = 5f;

        private Rigidbody2D _rigidbody;

        // Desired movement direction for the next physics update.
        private Vector2 _moveDirection;

        /// <summary>
        /// Current Rigidbody velocity.
        /// Other systems (animation, AI, etc.) can observe this
        /// without accessing the Rigidbody directly.
        /// </summary>
        public Vector2 Velocity => _rigidbody.linearVelocity;

        /// <summary>
        /// Last non-zero movement direction.
        /// Used when the character is idle but should continue
        /// facing the previous direction.
        /// </summary>
        public Vector2 FacingDirection { get; private set; } = Vector2.down;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();

            // NOTE:
            // Caching component references in Awake() is the conventional
            // Unity approach and avoids repeated GetComponent() calls.

            // PERF:
            // Later we'll explore dependency injection or a composition
            // root to provide dependencies without GetComponent().
        }

        /// <summary>
        /// Receives a movement command from an external controller.
        /// </summary>
        public void SetMoveDirection(Vector2 direction)
        {
            // ARCH:
            // Movement never decides where to move.
            // Another component (PlayerController, EnemyController, etc.)
            // is responsible for making that decision.

            // Normalize to prevent faster diagonal movement.
            _moveDirection = direction.normalized;

            // Remember the last movement direction.
            if (_moveDirection != Vector2.zero)
            {
                FacingDirection = _moveDirection;
            }

            // PERF:
            // If movement commands are guaranteed to already be normalized,
            // this normalization step can be skipped.
        }

        private void FixedUpdate()
        {
            // NOTE:
            // Physics movement belongs in FixedUpdate() to stay synchronized
            // with Unity's physics simulation.

            _rigidbody.linearVelocity = _moveDirection * _moveSpeed;

            // PERF:
            // Every moving object currently owns its own FixedUpdate().
            // During the optimization phase we'll compare this with a
            // centralized update loop (DOP/ECS-inspired architecture).
        }
    }
}