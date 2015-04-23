namespace Astrid.Gui
{
    public abstract class ScreenLayer
    {
        protected ScreenLayer(Camera camera)
        {
            Camera = camera;
        }

        public Camera Camera { get; private set; }
        public virtual void Update(float deltaTime, InputDevice inputDevice) { }
        public abstract void Render(float deltaTime);
    }
}