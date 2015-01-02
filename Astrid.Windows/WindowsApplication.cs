﻿using Astrid.Framework;
using Astrid.Framework.Assets;
using Astrid.Framework.Audio;
using Astrid.Framework.Graphics;
using Astrid.Framework.Input;
using Astrid.Windows.Assets;
using Astrid.Windows.Audio;
using Astrid.Windows.Graphics;
using Astrid.Windows.Input;

namespace Astrid.Windows
{
    public class WindowsApplication : ApplicationBase
    {
        public WindowsApplication(WindowsApplicationConfig config) 
        {
            _config = config;
        }

        private readonly WindowsApplicationConfig _config;
        
        public override AssetManager CreateAssetManager()
        {
            return new WindowsAssetManager(_config.ContentPath);
        }

        private GLGraphicsDevice _graphicsDevice;
        public override GraphicsDevice CreateGraphicsDevice()
        {
            _graphicsDevice = new GLGraphicsDevice();
            return _graphicsDevice;
        }

        private TKInputDevice _inputDevice;
        public override InputDevice CreateInputDevice()
        {
            _inputDevice = new TKInputDevice();
            return _inputDevice;
        }

        private NAudioDevice _audioDevice;
        public override AudioDevice CreateAudioDevice()
        {
            _audioDevice = new NAudioDevice();
            return _audioDevice;
        }

        public override void Run(GameBase game)
        {
            using (var gameWindow = new WindowsGameWindow(game, _graphicsDevice, _config))
            {
                gameWindow.MouseMove += (sender, args) => _inputDevice.SetPosition(args.X, args.Y);
                gameWindow.Run();
            }
        }
    }
}
