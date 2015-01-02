namespace Astrid.Windows
{
    public class WindowsApplicationConfig
    {
        public WindowsApplicationConfig()
        {
            ContentPath = "Content";
        }

        public string Title { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public string ContentPath { get; set; }
    }
}