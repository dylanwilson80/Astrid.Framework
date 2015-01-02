using System;
using Astrid.Framework;
using Astrid.Windows.Graphics;
using OpenTK;

namespace Astrid.Windows
{
    public class WindowsGameWindow : GameWindow
    {
        public WindowsGameWindow(GameBase game, GLGraphicsDevice graphicsDevice, WindowsApplicationConfig config)
            : base(config.Width, config.Height)
        {
            _game = game;
            _graphicsDevice = graphicsDevice;

            Title = config.Title;
        }

        private readonly GameBase _game;
        private readonly GLGraphicsDevice _graphicsDevice;

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            _graphicsDevice.Resize(Width, Height);
            _game.Resize(Width, Height);
        }

        protected override void OnFocusedChanged(EventArgs e)
        {
            base.OnFocusedChanged(e);

            if (Focused)
                _game.Resume();
            else
                _game.Pause();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            VSync = VSyncMode.On;

            _graphicsDevice.Initialize();
            _game.Create();
        }

        public override void Dispose()
        {
            _game.Destroy();
            base.Dispose();
        }

        // http://gafferongames.com/game-physics/fix-your-timestep/

        private const float _timeStep = 1.0f / 60.0f;
        private float _accumulator;

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            var deltaTime = (float) e.Time;

            _accumulator += deltaTime;

            while (_accumulator >= _timeStep)
            {
                _game.Update(_timeStep);
                _accumulator -= _timeStep;
            }
            
            base.OnUpdateFrame(e);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            var deltaTime = (float)e.Time;
            _game.Render(deltaTime);
            SwapBuffers();
        }
    }
}
