using System.Diagnostics;
using Astrid.Windows;

namespace AstridDemo.Windows
{
    public class PlatformService : IPlatformService
    {
        public void OpenUrl(string url)
        {
            Process.Start(url);
        }
    }

    class Program
    {
        static void Main()
        {
            var config = new WindowsApplicationConfig
            {
                Title = "Astrid Demo",
                Width = 800,
                Height = 480,
            };

            using (var application = new WindowsApplication(config))
            {
                var game = new DemoGame(application, new PlatformService());
                application.Run(game);
            }
        }
    }
}
