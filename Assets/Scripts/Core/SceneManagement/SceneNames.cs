namespace Game.Core
{
    /// <summary>
    /// Centralized list of scene names used throughout the project.
    ///
    /// NOTE:
    /// Unity loads scenes by their name, so keeping all names here avoids
    /// scattered string literals and reduces the chance of typos.
    ///
    /// Remember to add these scenes to Build Settings.
    /// </summary>
    public static class SceneNames
    {
        public const string Bootstrap = "Bootstrap";
        public const string MainMenu = "MainMenu";
        public const string TestScene = "TestScene";
    }
}
