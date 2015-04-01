using Astrid.Framework;
using Astrid.Gui;

namespace Astrid.Engine
{
    public abstract class GameBase : IApplicationListener, IScreenManager
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

        private Screen _currentScreen;

        public void SetScreen(Screen newScreen)
        {
            if (_currentScreen != null)
            {
                _currentScreen.Hide();
                _currentScreen.Unload();
            }

            _currentScreen = newScreen;

            if (_currentScreen != null)
            {
                _currentScreen.Load();
                _currentScreen.Show();
            }
        }

        public abstract void Create();
        public abstract void Destroy();

        public virtual void Pause()
        {
            if (_currentScreen != null)
                _currentScreen.Pause();
        }

        public virtual void Resume()
        {
            if (_currentScreen != null)
                _currentScreen.Resume();
        }

        public virtual void Resize(int width, int height)
        {
            if (_currentScreen != null)
                _currentScreen.Resize(width, height);
        }

        public virtual void Update(float deltaTime)
        {
            if (_currentScreen != null)
                _currentScreen.Update(deltaTime);
        }

        public virtual void Render(float deltaTime)
        {
            if (_currentScreen != null)
                _currentScreen.Render(deltaTime);
        }
    }
}
