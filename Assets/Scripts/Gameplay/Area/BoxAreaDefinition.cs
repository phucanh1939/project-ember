using UnityEngine;
using Game.Core;

namespace Game.Gameplay
{
    /// <summary>
    /// Defines a rectangular area used for target queries.
    /// </summary>
    [CreateAssetMenu(menuName = "Gameplay/Areas/Box")]
    public class BoxAreaDefinition : AreaDefinition
    {
        [SerializeField] private Vector2 _offset;
        [SerializeField] private Vector2 _size;

        public override Collider2D[] Query(Vector2 position, Vector2 direction)
        {
            var angle = Vector2Utils.DirectionToAngle(direction);
            var center = position + Vector2Utils.RotateByAngle(_offset, angle);
            return Physics2D.OverlapBoxAll(center, _size, angle);
        }
    }
}