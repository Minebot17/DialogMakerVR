namespace DialogCommon.Utils
{
    public enum Scenes
    {
        MainMenu, MakerScene
    }
    
    public enum PlayerScenes
    {
        DoctorsAppointment, Registration
    }

    public static class ScenesEnumExtensions
    {
        public static string GetName(this Scenes scene)
        {
            return scene.ToString("G");
        }
        
        public static string GetName(this PlayerScenes scene)
        {
            return scene.ToString("G");
        }
    }
}