using UnityEngine;
using Game.Core;

namespace Game.Player
{
    /// <summary>
    /// Wires Player dependencies at scene start.
    /// This is part of manual dependency injection for Core services.
    /// </summary>
    [RequireComponent(typeof(PlayerInput))]

    public class PlayerInstaller : MonoBehaviour
    {
        [SerializeField] private PlayerInput playerInput;

        private void Awake()
        {
            // Ensure InputService exists
            var inputService = InputService.Instance;

            if (inputService == null)
            {
                Debug.LogError("[PlayerInstaller] InputService not found in scene!");
                return;
            }

            // Inject dependencies
            playerInput.Initialize(inputService);
        }
    }
}