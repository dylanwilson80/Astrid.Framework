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
                Title = "Astrid Demo",
                Width = 800,
                Height = 480,
            };

            using (var application = new WindowsApplication(config))
            {
                var game = new LibGdxTextureAtlasDemo(application);
                application.Run(game);
            }
        }
    }
}
