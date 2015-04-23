using Astrid.Core;

namespace Astrid
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

        protected override Vector2 CalculateScale()
        {
            return Vector2.One;
        }
    }
}