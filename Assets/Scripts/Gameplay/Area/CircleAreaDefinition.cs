using UnityEngine;

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

        public override Collider2D[] Query(Transform origin)
        {
            var center = origin.TransformPoint(_offset);
            return Physics2D.OverlapCircleAll(center, _radius);
        }
    }
}