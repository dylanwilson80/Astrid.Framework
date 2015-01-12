using Astrid.Framework.Entities;
using Astrid.Framework.Graphics;

namespace Astrid.MonoGame
{
    public class MonoGameComponentSystemFactory : ComponentSystemFactory
    {
        public MonoGameComponentSystemFactory(MonoGameDeviceManager deviceManager, Camera camera)
            : base(deviceManager, camera)
        {
        }
    }
}