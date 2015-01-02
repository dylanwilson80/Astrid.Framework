using System;
using System.Collections.Generic;
using System.Linq;
using Astrid.Core;
using Astrid.Framework.Assets;
using Astrid.Framework.Entities.Components;
using Astrid.Framework.Entities.Systems;
using Common.Logging;

namespace Astrid.Framework.Entities
{
    public class ComponentSystemNotRegisteredException : Exception
    {
        public ComponentSystemNotRegisteredException(Type componentType)
            : base(string.Format("Component system not registered for type {0}", componentType))
        {
        }
    }

    public class EntitySpace
    {
        private static readonly ILog _logger = LogManager.GetCurrentClassLogger();

        internal EntitySpace(string name, AssetManager assetManager, IComponentSystemFactory componentSystemFactory)
        {
            _assetManager = assetManager;
            _componentSystemFactory = componentSystemFactory;
            _systems = new Dictionary<Type, ComponentSystem>();

            Name = name;
        }

        public string Name { get; private set; }

        private readonly Dictionary<Type, ComponentSystem> _systems;
        private readonly AssetManager _assetManager;
        private readonly IComponentSystemFactory _componentSystemFactory;

        public void RegisterSystem(ComponentSystem system)
        {
            _logger.Info(string.Format("Registering system {0}", system));

            _systems.Add(system.ComponentType, system);
        }

        public void DeregisterSystem(ComponentSystem system)
        {
            _logger.Info(string.Format("Deregistering system {0}", system));

            _systems.Remove(system.ComponentType);
        }

        public void Update(float deltaTime)
        {
            foreach (var system in _systems.Values)
                system.Update(deltaTime);
        }

        public void Draw(float deltaTime)
        {
            var drawableSystems = _systems.Values
                .Where(i => i is DrawableSystem)
                .Cast<DrawableSystem>();
            
            foreach (var system in drawableSystems)
                system.Draw(deltaTime);
        }

        internal IEnumerable<ComponentSystem> GetSystemsForType(Type componentType)
        {
            var currentComponentType = componentType;

            while (currentComponentType != null)
            {
                ComponentSystem system;

                if (_systems.TryGetValue(currentComponentType, out system))
                {
                    yield return system;
                }
                else if(_componentSystemFactory != null)
                {
                    system = _componentSystemFactory.CreateSystemForType(currentComponentType);

                    if (system != null)
                    {
                        _systems.Add(currentComponentType, system);
                        yield return system;
                    }
                }

                currentComponentType = currentComponentType.BaseType;
            }
        }

        internal IEnumerable<ComponentSystem> GetSystemForType<T>() 
            where T : Component
        {
            return GetSystemsForType(typeof(T));
        }
        
        public Entity CreateEntity()
        {
            return CreateEntity(string.Empty, Vector2.Zero, 0, Vector2.One);
        }

        public Entity CreateEntity(Vector2 position)
        {
            return CreateEntity(string.Empty, position, 0, Vector2.One);
        }

        public Entity CreateEntity(string name)
        {
            return CreateEntity(name, Vector2.Zero, 0, Vector2.One);
        }

        public Entity CreateEntity(string name, Vector2 position, float rotation, Vector2 scale)
        {
            return new Entity(this, name, position, rotation, scale);
        }

        public void RemoveEntity(Entity entity)
        {
            // TODO: Logging?
        }
    }
}
