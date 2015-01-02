using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                var game = new DemoGame(application);
                application.Run(game);
            }
        }
    }
}
