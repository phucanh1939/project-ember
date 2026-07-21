using UnityEngine;

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

        public override Collider2D[] Query(Transform origin)
        {
            var center = origin.TransformPoint(_offset);
            var angle = origin.eulerAngles.z;
            return Physics2D.OverlapBoxAll(center, _size, angle);
        }
    }
}