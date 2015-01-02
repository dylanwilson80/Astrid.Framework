using System;
using Astrid.Core;
using Astrid.Framework.Entities.Components;
using Astrid.Framework.Entities.Components.Gui;
using Astrid.Framework.Entities.Systems;
using Astrid.Framework.Graphics;

namespace Astrid.Framework.Entities
{
    public class DefaultComponentSystemFactory : IComponentSystemFactory
    {
        private readonly GameBase _game;
        private readonly Camera _camera;

        public DefaultComponentSystemFactory(GameBase game, Camera camera)
        {
            _game = game;
            _camera = camera;
        }

        public ComponentSystem CreateSystemForType(Type type)
        {
            if (type == typeof(Drawable))
                return new DrawableSystem(_game.GraphicsDevice, _camera);

            if (type == typeof (GuiControl))
                return new GuiSystem(_game.InputDevice);

            if (type == typeof (PhysicsComponent))
                return new PhysicsSystem(new Vector2(0, 10));

            return null;
            //throw new InvalidOperationException(string.Format("DefaultComponentSystemFactory does not support component type {0}", type));
        }
    }
}