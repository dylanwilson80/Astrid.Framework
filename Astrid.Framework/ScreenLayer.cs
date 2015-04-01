namespace Astrid.Framework
{
    public abstract class ScreenLayer
    {
        public virtual void Update(float deltaTime, InputDevice inputDevice) { }
        public abstract void Render(float deltaTime);
    }
}