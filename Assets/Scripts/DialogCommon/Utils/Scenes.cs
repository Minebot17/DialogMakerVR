namespace DialogCommon.Utils
{
    public enum Scenes
    {
        MainMenu, MakerScene, PlayerScene
    }

    public static class ScenesEnumExtensions
    {
        public static string GetName(this Scenes scene)
        {
            return scene.ToString("G");
        }
    }
}