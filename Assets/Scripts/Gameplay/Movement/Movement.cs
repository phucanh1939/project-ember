using System;
using UnityEngine;

namespace Game.Gameplay
{
    /// <summary>
    /// Responsible for moving a character using Rigidbody2D.
    ///
    /// This component only executes movement commands.
    /// It does not decide where the character should move.
    ///
    /// It also exposes movement state changes (such as facing direction)
    /// so visual systems can react without polling every frame.
    /// </summary>
    [RequireComponent(typeof(Rigidbody2D))]
    public class Movement : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody;

        private IMovementSpeedProvider _speedProvider;
        private Vector2 _moveDirection; // Desired movement direction for the next physics update.

        public Vector2 Velocity => _rigidbody.linearVelocity;
        public Vector2 FacingDirection { get; private set; } = Vector2.down;
        public event Action<Vector2> OnFacingDirectionChanged;

        private void OnValidate()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        public void Initialize(IMovementSpeedProvider speedProvider)
        {
            _speedProvider = speedProvider;
        }

        public void SetMoveDirection(Vector2 direction)
        {
            _moveDirection = direction.normalized;

            if (_moveDirection != Vector2.zero)
            {
                UpdateFacingDirection(_moveDirection);
            }

            // PERF:
            // If movement commands are guaranteed to already be normalized,
            // this normalization step can be skipped.
        }

        public void StopMovement()
        {
            _moveDirection = Vector2.zero;
        }

        private void UpdateFacingDirection(Vector2 direction)
        {
            if (FacingDirection == direction)
                return;

            FacingDirection = direction;

            OnFacingDirectionChanged?.Invoke(FacingDirection);
        }

        private void FixedUpdate()
        {
            // PERF:
            // Every moving object currently owns its own FixedUpdate().
            // During the optimization phase we'll compare this with a
            // centralized update loop (DOP/ECS-inspired architecture).
            _rigidbody.linearVelocity = _moveDirection * _speedProvider.MoveSpeed;
        }
        
    }
}