using System;
using Astrid.Components.Components;
using Astrid.Components.Systems;
using Astrid.Framework;

namespace Astrid.Components
{


    public class ComponentSystemFactory
    {
        private readonly IDeviceManager _deviceManager;
        private readonly Camera _camera;

        public ComponentSystemFactory(IDeviceManager deviceManager, Camera camera)
        {
            _deviceManager = deviceManager;
            _camera = camera;
        }

        public virtual ComponentSystem CreateSystemForType(Type type)
        {
            if (type == typeof(Drawable))
                return new DrawableSystem(_deviceManager.GraphicsDevice, _camera);

            //if (type == typeof (GuiControl))
            //    return new GuiSystem(_deviceManager.InputDevice);

            //if (type == typeof (PhysicsComponent))
            //    return new PhysicsSystem(new Vector2(0, 10));

            return null;
            //throw new InvalidOperationException(string.Format("ComponentSystemFactory does not support component type {0}", type));
        }
    }
}