using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Core
{
    /// <summary>
    /// Entry point of the application.
    ///
    /// Responsible for initializing core systems before loading the first gameplay scene.
    /// This component should never contain gameplay logic.
    /// </summary>
    public class Bootstrap : MonoBehaviour
    {
        [Header("Startup")]
        [SerializeField]
        private string startupScene = "TestScene";

        private void Awake()
        {
            InitializeCoreSystems();
            LoadStartupScene();
        }

        /// <summary>
        /// Initializes all core services required by the game.
        /// </summary>
        /// <remarks>
        /// ARCH:
        /// As the project grows, initialize systems such as:
        /// - Audio
        /// - Save/Load
        /// - Localization
        /// - Analytics
        /// - Addressables
        /// - Dependency Injection / Service Locator
        ///
        /// Keep gameplay systems out of here.
        /// </remarks>
        private void InitializeCoreSystems()
        {
            Debug.Log("[Bootstrap] Initializing Core Systems...");
        }

        /// <summary>
        /// Loads the first scene after initialization.
        /// </summary>
        private void LoadStartupScene()
        {
            Debug.Log($"[Bootstrap] Loading '{startupScene}'...");

            SceneManager.LoadScene(startupScene);
        }
    }
}