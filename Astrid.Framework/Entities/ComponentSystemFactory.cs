using System;
using Astrid.Core;
using Astrid.Framework.Audio;
using Astrid.Framework.Entities.Components;
using Astrid.Framework.Entities.Components.Gui;
using Astrid.Framework.Entities.Systems;
using Astrid.Framework.Graphics;
using Astrid.Framework.Input;

namespace Astrid.Framework.Entities
{
    public interface IDeviceManager
    {
        GraphicsDevice GraphicsDevice { get; }
        InputDevice InputDevice { get; }
        AudioDevice AudioDevice { get; }
    }

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