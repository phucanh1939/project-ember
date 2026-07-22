using UnityEngine;
using Game.Core;

namespace Game.Gameplay
{
    /// <summary>
    /// Defines a circular area used for target queries.
    /// </summary>
    [CreateAssetMenu(menuName = "Gameplay/Areas/Circle")]
    public class CircleAreaDefinition : AreaDefinition
    {
        [SerializeField] private Vector2 _offset;
        [SerializeField] private float _radius;

        public override Collider2D[] Query(Vector2 position, Vector2 direction)
        {
            var angle = Vector2Utils.DirectionToAngle(direction);
            var center = position + Vector2Utils.RotateByAngle(_offset, angle);
            return Physics2D.OverlapCircleAll(center, _radius);
        }
    }
}