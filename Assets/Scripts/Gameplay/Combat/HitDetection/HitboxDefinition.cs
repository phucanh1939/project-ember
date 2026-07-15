using UnityEngine;

namespace Game.Gameplay
{
    [CreateAssetMenu(menuName = "Game/Combat/Hitbox Definition")]
    /// <summary>
    /// Defines the local size and offset of a hitbox.
    /// </summary>
    public class HitboxDefinition : ScriptableObject
    {
        [SerializeField] private Vector2 _offset;
        [SerializeField] private Vector2 _size;

        public Vector2 Offset => _offset;
        public Vector2 Size => _size;
    }
}
