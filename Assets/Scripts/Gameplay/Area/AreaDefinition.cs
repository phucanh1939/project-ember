using UnityEngine;

namespace Game.Gameplay
{
    public abstract class AreaDefinition : ScriptableObject
    {
        public abstract Collider2D[] Query(Vector2 position, Vector2 direction);
    }
}

