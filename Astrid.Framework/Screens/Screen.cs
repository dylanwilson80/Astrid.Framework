using System;
using System.Collections.Generic;
using Astrid.Core;
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
            _layers = new List<ScreenLayer>();

            ClearColor = Color.CornflowerBlue;
            Viewport = new StretchViewport(GraphicsDevice, GraphicsDevice.Width, GraphicsDevice.Height);
        }

        private readonly GameBase _game;

        public Color ClearColor { get; set; }
        public Viewport Viewport { get; set; }
        
        private readonly List<ScreenLayer> _layers;
        public IList<ScreenLayer> Layers
        {
            get { return _layers; }
        }

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
        public virtual void Update(float deltaTime) { }

        public virtual void Render(float deltaTime)
        {
            GraphicsDevice.Clear(ClearColor);

            foreach (var layer in _layers)
                layer.Render(deltaTime);
        }

        public virtual void Dispose() { }
    }
}
