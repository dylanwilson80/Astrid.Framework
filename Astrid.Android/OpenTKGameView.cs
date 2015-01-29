using System;
using Android.Content;
using Android.Graphics;
using Android.Views;
using Astrid.Framework;
using Astrid.Windows.Graphics;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.ES20;
using OpenTK.Platform.Android;

namespace Astrid.Android
{
    public class OpenTKGameView : AndroidGameView
    {
        private readonly GameBase _game;
        private readonly GLGraphicsDevice _graphicsDevice;

        internal OpenTKGameView(Context context, GameBase game, GLGraphicsDevice graphicsDevice, AndroidApplicationConfig config)
            : base(context)
        {
            _game = game;
            _graphicsDevice = graphicsDevice;
            ContextRenderingApi = GLVersion.ES2;
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            _graphicsDevice.Resize(Width, Height);
            _game.Resize(Width, Height);
        }

        protected override void OnFocusChanged(bool gainFocus, FocusSearchDirection direction, Rect previouslyFocusedRect)
        {
            base.OnFocusChanged(gainFocus, direction, previouslyFocusedRect);

            if (gainFocus)
                _game.Resume();
            else
                _game.Pause();
        }

        // TODO: Should this go in OnLoad or OnCreateFrameBuffer
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            _graphicsDevice.Initialize();
            _game.Create();

            Run();
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);
            _game.Render((float)e.Time);
            SwapBuffers();
        }
    }
}