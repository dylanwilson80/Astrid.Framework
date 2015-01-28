using System;
using Astrid.Framework;
using Astrid.Framework.Assets;
using Astrid.Framework.Audio;
using Astrid.Framework.Graphics;
using Astrid.Framework.Input;
using Astrid.Windows.Graphics;
using OpenTK;
using OpenTK.Platform.Android;

namespace Astrid.Android
{
    public class AndroidApplication : ApplicationBase
    {
        private readonly AndroidApplicationConfig _config;

        public AndroidApplication(AndroidApplicationConfig config)
        {
            _config = config;
        }

        private OpenTKGameView _view;
        public AndroidGameView View
        {
            get { return _view; }
        }

        public override AssetManager CreateAssetManager()
        {
            return new AndroidAssetManager();
        }

        private GLGraphicsDevice _graphicsDevice;
        public override GraphicsDevice CreateGraphicsDevice()
        {
            _graphicsDevice = new GLGraphicsDevice(800, 480);
            return _graphicsDevice;
        }

        public override InputDevice CreateInputDevice()
        {
            return new AndroidInputDevice();
        }

        public override AudioDevice CreateAudioDevice()
        {
            return new AndroidAudioDevice();
        }

        public override void Run(GameBase game)
        {
            _view = new OpenTKGameView(_config.Activity, game, _graphicsDevice, _config);
            _view.RequestFocus();
            _view.FocusableInTouchMode = true;
        }

        public void Pause()
        {
            _view.Pause();
        }

        public void Resume()
        {
            _view.Resume();
        }
    }
}
