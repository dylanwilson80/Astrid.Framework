﻿using Astrid.Windows;

namespace AstridDemo.Windows
{
    class Program
    {
        static void Main()
        {
            var config = new WindowsApplicationConfig
            {
                WindowTitle = "Astrid Demo",
                WindowWidth = 360,
                WindowHeight = 600,
            };

            using (var application = new WindowsApplication(config))
            using (var game = new DemoGame(application))
            {
                application.Run(game);
            }
        }
    }
}
