using Astrid.Framework.Assets;
using Astrid.Framework.Audio;
using Astrid.Framework.Entities;
using Astrid.Framework.Graphics;
using Astrid.Framework.Input;

namespace Astrid.Framework
{
    public abstract class GameBase : IDeviceManager
    {
        protected GameBase(ApplicationBase application)
        {
            AssetManager = application.CreateAssetManager();
            GraphicsDevice = application.CreateGraphicsDevice();
            InputDevice = application.CreateInputDevice();
            AudioDevice = application.CreateAudioDevice();
        }

        public AssetManager AssetManager { get; private set; }
        public GraphicsDevice GraphicsDevice { get; private set; }
        public InputDevice InputDevice { get; private set; }
        public AudioDevice AudioDevice { get; private set; }

        public abstract void Create();
        public abstract void Destroy();
        public abstract void Pause();
        public abstract void Resume();
        public abstract void Resize(int width, int height);
        public abstract void Update(float deltaTime);
        public abstract void Render(float deltaTime);
    }
}
