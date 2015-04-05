using Astrid.Framework;

namespace Astrid.Gui
{
    public abstract class ScreenLayer
    {
        protected ScreenLayer(Viewport viewport)
        {
            Viewport = viewport;
        }

        public Viewport Viewport { get; private set; }
        public virtual void Update(float deltaTime, InputDevice inputDevice) { }
        public abstract void Render(float deltaTime);
    }
}