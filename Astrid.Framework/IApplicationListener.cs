namespace Astrid.Framework
{
    public interface IDeviceManager
    {
        GraphicsDevice GraphicsDevice { get; }
        InputDevice InputDevice { get; }
        AudioDevice AudioDevice { get; }
    }

    public interface IApplicationListener : IDeviceManager
    {
        void Create();
        void Destroy();
        void Pause();
        void Resume();
        void Resize(int width, int height);
        void Update(float deltaTime);
        void Render(float deltaTime);
    }
}