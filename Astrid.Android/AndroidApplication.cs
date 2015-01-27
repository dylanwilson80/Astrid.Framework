using Astrid.Framework;
using Astrid.Framework.Assets;
using Astrid.Framework.Audio;
using Astrid.Framework.Graphics;
using Astrid.Framework.Input;
using OpenTK.Platform.Android;

namespace Astrid.Android
{
    public class AndroidApplication : ApplicationBase
    {
        private readonly AndroidApplicationConfig _config;
        private readonly OpenTKGameView _view;

        public AndroidApplication(AndroidApplicationConfig config)
        {
            _config = config;
            _view = new OpenTKGameView(config.Activity);
        }

        public AndroidGameView View { get { return _view; } }

        public override AssetManager CreateAssetManager()
        {
            return new AndroidAssetManager();
        }

        public override GraphicsDevice CreateGraphicsDevice()
        {
            return new AndroidGraphicsDevice(_view.Width, _view.Height);
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
