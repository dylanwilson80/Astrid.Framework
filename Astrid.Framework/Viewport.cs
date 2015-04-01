using Astrid.Core;

namespace Astrid.Framework
{
    public abstract class Viewport
    {
        protected Viewport(GraphicsDevice graphicsDevice)
            : this(graphicsDevice, new Camera())
        {
        }

        protected Viewport(GraphicsDevice graphicsDevice, Camera camera)
        {
            GraphicsDevice = graphicsDevice; 
            Camera = camera;
            graphicsDevice.Resized += (sender, args) => camera.Scale = GetCameraScale();
        }

        public GraphicsDevice GraphicsDevice { get; private set; }
        public Camera Camera { get; set; }
        public abstract int Width { get; }
        public abstract int Height { get; }

        protected abstract Vector2 GetCameraScale();
    }
}
