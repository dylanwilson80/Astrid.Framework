namespace Astrid
{
    public interface IDeviceManager
    {
        GraphicsDevice GraphicsDevice { get; }
        InputDevice InputDevice { get; }
        AudioDevice AudioDevice { get; }
    }
}