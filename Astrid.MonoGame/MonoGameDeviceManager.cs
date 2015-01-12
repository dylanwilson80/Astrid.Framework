using Astrid.Framework.Audio;
using Astrid.Framework.Entities;
using Astrid.Framework.Graphics;
using Astrid.Framework.Input;

namespace Astrid.MonoGame
{
    public class MonoGameDeviceManager : IDeviceManager
    {
        public MonoGameDeviceManager(Microsoft.Xna.Framework.Graphics.GraphicsDevice graphicsDevice, Camera camera)
        {
            GraphicsDevice = new MonoGameGraphicsDevice(graphicsDevice, camera);
        }

        public GraphicsDevice GraphicsDevice { get; private set; }
        public InputDevice InputDevice { get; private set; }
        public AudioDevice AudioDevice { get; private set; }
    }
}