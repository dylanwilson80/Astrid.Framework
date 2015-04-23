namespace Astrid.Windows
{
    public class WindowsApplicationConfig
    {
        public WindowsApplicationConfig()
        {
            ContentPath = "Content";
        }

        public string WindowTitle { get; set; }
        public int WindowWidth { get; set; }
        public int WindowHeight { get; set; }
        public string ContentPath { get; set; }
    }
}