using UnityEngine.SceneManagement;

namespace Game.Core
{
    /// <summary>
    /// Provides a centralized API for scene loading.
    ///
    /// ARCH:
    /// Gameplay systems should never call Unity's SceneManager directly.
    /// They should use SceneService instead.
    ///
    /// This allows scene transitions, loading screens, asynchronous loading,
    /// and other features to be added later without changing gameplay code.
    /// </summary>
    public static class SceneService
    {
        /// <summary>
        /// Gets the name of the currently active scene.
        /// </summary>
        public static string CurrentScene =>
            SceneManager.GetActiveScene().name;

        /// <summary>
        /// Loads a scene immediately.
        /// </summary>
        /// <param name="sceneName">Name of the scene to load.</param>
        public static void Load(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }

        /// <summary>
        /// Reloads the current scene.
        /// </summary>
        public static void ReloadCurrent()
        {
            Load(CurrentScene);
        }

        /// <summary>
        /// Loads the main menu.
        /// </summary>
        public static void LoadMainMenu()
        {
            Load(SceneNames.MainMenu);
        }
    }
}
