using Astrid.Framework.Input;

namespace Astrid.Framework.Screens
{
    public abstract class ScreenLayer
    {
        public virtual void Update(float deltaTime, InputDevice inputDevice) { }
        public abstract void Render(float deltaTime);
    }
}