using System;
using Astrid.Gui;

namespace Astrid
{
    public abstract class GameBase 
        : IDisposable, IApplicationListener, IScreenManager, IInputDeviceContext
    {
        protected GameBase(ApplicationBase application)
        {
            GraphicsDevice = application.CreateGraphicsDevice();
            InputDevice = application.CreateInputDevice(this);
            AudioDevice = application.CreateAudioDevice();
            AssetManager = application.CreateAssetManager(this);
            Viewport = new StretchViewport(GraphicsDevice, GraphicsDevice.Width, GraphicsDevice.Height);
        }

        public Viewport Viewport { get; set; }
        public AssetManager AssetManager { get; private set; }
        public GraphicsDevice GraphicsDevice { get; private set; }
        public InputDevice InputDevice { get; private set; }
        public AudioDevice AudioDevice { get; private set; }

        private Screen _currentScreen;

        public void SetScreen(Screen newScreen)
        {
            if (_currentScreen != null)
                _currentScreen.Hide();

            _currentScreen = newScreen;

            if (_currentScreen != null)
                _currentScreen.Show();
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

        public void Dispose()
        {
            if (_currentScreen != null)
                _currentScreen.Hide();

            AudioDevice.Dispose();
        }
    }
}
