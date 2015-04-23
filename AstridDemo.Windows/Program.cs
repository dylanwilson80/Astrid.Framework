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
                WindowWidth = (int)(800 * 0.75f),
                WindowHeight = (int)(480 * 0.75f),
            };

            using (var application = new WindowsApplication(config))
            using (var game = new DemoGame(application))
            {
                application.Run(game);
            }
        }
    }
}
