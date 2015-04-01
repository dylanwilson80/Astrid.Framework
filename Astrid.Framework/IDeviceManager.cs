namespace Astrid.Framework
{
    public interface IDeviceManager
    {
        GraphicsDevice GraphicsDevice { get; }
        InputDevice InputDevice { get; }
        AudioDevice AudioDevice { get; }
    }
}