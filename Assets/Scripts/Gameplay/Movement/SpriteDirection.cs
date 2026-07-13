using UnityEngine;

namespace Game.Gameplay
{
    /// <summary>
    /// Controls sprite facing direction based on character movement.
    ///
    /// This component only handles visual orientation.
    /// It does not control movement or gameplay.
    /// </summary>
    [RequireComponent(typeof(SpriteRenderer))]
    public class SpriteDirection : MonoBehaviour
    {
        [SerializeField] private Movement _movement;
        [SerializeField] private SpriteRenderer _spriteRenderer;

        private void OnValidate()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
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
            if (direction.x != 0)
            {
                _spriteRenderer.flipX = direction.x < 0;
            }
        }
    }
}