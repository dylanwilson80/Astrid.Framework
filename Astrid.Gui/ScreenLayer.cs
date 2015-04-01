using Astrid.Framework;

namespace Astrid.Gui
{
    public abstract class ScreenLayer
    {
        public virtual void Update(float deltaTime, InputDevice inputDevice) { }
        public abstract void Render(float deltaTime);
    }
}