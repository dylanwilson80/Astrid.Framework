using System;
using Astrid.Framework.Assets;
using Astrid.Framework.Audio;
using Astrid.Framework.Entities;
using Astrid.Framework.Graphics;
using Astrid.Framework.Input;

namespace Astrid.Framework.Screens
{
    public abstract class Screen : IDisposable, IDeviceManager
    {
        protected Screen(GameBase game)
        {
            _game = game;
        }

        private readonly GameBase _game;

        public AssetManager AssetManager
        {
            get { return _game.AssetManager; }
        }

        public GraphicsDevice GraphicsDevice
        {
            get { return _game.GraphicsDevice; }
        }

        public InputDevice InputDevice
        {
            get { return _game.InputDevice; }
        }

        public AudioDevice AudioDevice
        {
            get { return _game.AudioDevice; }
        }

        public virtual void Load() { }
        public virtual void Unload() { }
        public virtual void Show() { }
        public virtual void Hide() { }
        public virtual void Resize(int width, int height) { }
        public virtual void Pause() { }
        public virtual void Resume() { }
        public abstract void Update(float deltaTime);
        public abstract void Render(float deltaTime);
        public virtual void Dispose() { }
    }
}
