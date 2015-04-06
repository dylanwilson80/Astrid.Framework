using Astrid.Core;

namespace Astrid.Gui
{
    public class StretchViewport : Viewport
    {
        public StretchViewport(GraphicsDevice graphicsDevice, int virtualWidth, int virtualHeight) 
            : base(graphicsDevice)
        {
            _virtualWidth = virtualWidth;
            _virtualHeight = virtualHeight;
        }

        private readonly int _virtualWidth;
        private readonly int _virtualHeight;

        public override int Width
        {
            get { return _virtualWidth; }
        }

        public override int Height
        {
            get { return _virtualHeight; }
        }

        protected override Vector2 GetCameraScale()
        {
            return new Vector2((float) GraphicsDevice.Width / _virtualWidth, (float) GraphicsDevice.Height / _virtualHeight);
        }
    }
}