using UnityEngine;

namespace Game.Gameplay
{
    public abstract class AreaDefinition : ScriptableObject
    {
        public abstract Collider2D[] Query(Transform origin);
    }
}

