using System.Diagnostics;
using Astrid.Windows;

namespace AstridDemo.Windows
{
    class Program
    {
        static void Main()
        {
            var config = new WindowsApplicationConfig
            {
                WindowTitle = "Astrid Demo",
                WindowWidth = 480,
                WindowHeight = 800,
            };

            using (var application = new WindowsApplication(config))
            using (var game = new DemoGame(application))
            {
                application.Run(game);
            }
        }
    }
}
