using System;
using Astrid.Framework.Entities;
using Astrid.Framework.Entities.Components;
using Astrid.Framework.Entities.Components.Gui;
using Astrid.Framework.Entities.Systems;
using Astrid.Framework.Graphics;

namespace Astrid.MonoGame
{
    public class MonoGameComponentSystemFactory : IComponentSystemFactory
    {
        private readonly Microsoft.Xna.Framework.Graphics.GraphicsDevice _graphicsDevice;

        public MonoGameComponentSystemFactory(Microsoft.Xna.Framework.Graphics.GraphicsDevice graphicsDevice)
        {
            _graphicsDevice = graphicsDevice;
        }

        public ComponentSystem CreateSystemForType(Type type)
        {
            var camera = new Camera();
            var graphicsDevice = new MonoGameGraphicsDevice(_graphicsDevice, camera);

            if (type == typeof (Drawable))
                return new DrawableSystem(graphicsDevice, camera);

            return null;
        }
    }
}