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
            throw new System.NotImplementedException();
        }

        public override GraphicsDevice CreateGraphicsDevice()
        {
            throw new System.NotImplementedException();
        }

        public override InputDevice CreateInputDevice()
        {
            throw new System.NotImplementedException();
        }

        public override AudioDevice CreateAudioDevice()
        {
            throw new System.NotImplementedException();
        }

        public override void Run(GameBase game)
        {
            throw new System.NotImplementedException();
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
