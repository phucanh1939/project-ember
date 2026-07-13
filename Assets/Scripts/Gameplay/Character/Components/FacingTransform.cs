using UnityEngine;

namespace Game.Gameplay
{
    /// <summary>
    /// Maintains a transform orientation aligned with the owner's facing direction.
    ///
    /// This component provides a directional reference point for gameplay systems
    /// that need to follow the character's facing direction.
    ///
    /// Examples:
    /// - Attack hitbox placement.
    /// - Projectile spawn direction.
    /// - Directional interaction checks.
    ///
    /// This component does not control the gameplay behavior of those systems.
    /// It only provides transform orientation.
    /// </summary>
    public class FacingTransform : MonoBehaviour
    {
        [SerializeField] private Movement _movement;

        private void Start()
        {
            UpdateFacing(_movement.FacingDirection);
        }

        private void OnEnable()
        {
            _movement.OnFacingDirectionChanged += UpdateFacing;
        }

        private void OnDisable()
        {
            _movement.OnFacingDirectionChanged -= UpdateFacing;
        }

        private void UpdateFacing(Vector2 direction)
        {
            transform.right = direction;
        }
    }
}