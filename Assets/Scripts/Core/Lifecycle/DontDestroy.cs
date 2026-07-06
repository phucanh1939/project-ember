using UnityEngine;

namespace Game.Core
{
    /// <summary>
    /// Marks a GameObject as persistent across scenes.
    /// Lifecycle responsibility is handled here instead of each service.
    /// </summary>
    public class DontDestroy : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
