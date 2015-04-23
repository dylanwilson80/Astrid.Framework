using Astrid.Core;

namespace Astrid
{
    public abstract class Viewport
    {
        protected Viewport(GraphicsDevice graphicsDevice)
        {
            GraphicsDevice = graphicsDevice;
            graphicsDevice.Resized += (sender, args) => Scale = CalculateScale();
        }

        public GraphicsDevice GraphicsDevice { get; private set; }
        public Vector2 Scale { get; private set; }
        public abstract int Width { get; }
        public abstract int Height { get; }

        protected abstract Vector2 CalculateScale();
    }
}
