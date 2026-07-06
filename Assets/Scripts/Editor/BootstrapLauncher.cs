#if UNITY_EDITOR

using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Editor
{
    /// <summary>
    /// Starts Play Mode from the Bootstrap scene regardless of the currently open scene.
    ///
    /// NOTE:
    /// - Press F5 to start the game from Bootstrap.
    /// - Press F5 again to stop Play Mode.
    /// - After Play Mode ends, the previously opened scene is restored.
    /// </summary>
    [InitializeOnLoad]
    public static class BootstrapLauncher
    {
        /// <summary>
        /// Path to the Bootstrap scene.
        /// </summary>
        private const string BootstrapScenePath = "Assets/Scenes/Bootstrap.unity";

        /// <summary>
        /// SessionState key used to remember the previously opened scene.
        /// </summary>
        private const string PreviousSceneKey = "BootstrapLauncher.PreviousScene";

        static BootstrapLauncher()
        {
            EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
        }

        // Shortcut:
        // F5
        //
        // Starts or stops Play Mode.
        // When starting, the Bootstrap scene is opened first so the game
        // always begins through the normal initialization flow.
        [MenuItem("Tools/Play From Bootstrap _F5")]
        private static void TogglePlayMode()
        {
            // Toggle Play Mode.
            if (EditorApplication.isPlaying)
            {
                EditorApplication.isPlaying = false;
                return;
            }

            // Save any modified scenes.
            if (!EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
                return;

            string currentScene = SceneManager.GetActiveScene().path;

            // Remember which scene we were editing.
            if (currentScene != BootstrapScenePath)
            {
                SessionState.SetString(PreviousSceneKey, currentScene);
            }

            Debug.Log($"[BootstrapLauncher] Starting from Bootstrap. Previous scene: {currentScene}");

            EditorSceneManager.OpenScene(BootstrapScenePath);

            EditorApplication.isPlaying = true;
        }

        private static void OnPlayModeStateChanged(PlayModeStateChange state)
        {
            if (state != PlayModeStateChange.EnteredEditMode)
                return;

            string previousScene = SessionState.GetString(PreviousSceneKey, string.Empty);

            if (string.IsNullOrEmpty(previousScene))
                return;

            Debug.Log($"[BootstrapLauncher] Restoring scene: {previousScene}");

            EditorSceneManager.OpenScene(previousScene);

            SessionState.EraseString(PreviousSceneKey);
        }
    }
}

#endif