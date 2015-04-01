using Astrid.Core;
using Astrid.Framework;

namespace Astrid.Gui
{
    public class ScreenViewport : Viewport
    {
        public ScreenViewport(GraphicsDevice graphicsDevice)
            : base(graphicsDevice)
        {
        }

        public override int Width
        {
            get { return GraphicsDevice.Width; }
        }

        public override int Height
        {
            get { return GraphicsDevice.Height; }
        }

        protected override Vector2 GetCameraScale()
        {
            return Vector2.One;
        }
    }
}