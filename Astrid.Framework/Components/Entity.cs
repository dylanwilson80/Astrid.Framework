using System;
using System.Collections.Generic;
using System.Linq;
using Astrid.Components.Components;
using Astrid.Core;
using Newtonsoft.Json;

namespace Astrid.Components
{
    public class Entity
    {
        public Entity(EntitySpace space, string name, Vector2 position, float rotation, Vector2 scale)
        {
            _space = space;
            _components = new List<Component>();

            Name = name;
            Position = position;
            Rotation = rotation;
            Scale = scale;
        }

        private readonly EntitySpace _space;
        private readonly List<Component> _components;

        public IEnumerable<Component> Components
        {
            get { return _components; }
        }

        public string Name { get; private set; }

        private Vector2 _position;
        public Vector2 Position
        {
            get { return _position; }
            set
            {
                if (_position != value)
                {
                    _position = value;
                    PositionChanged.Raise(this, EventArgs.Empty);
                }
            }
        }

        [JsonIgnore]
        public EventHandler PositionChanged;

        private float _rotation;
        public float Rotation
        {
            get { return _rotation; }
            set
            {
                // ReSharper disable once CompareOfFloatsByEqualityOperator
                if (_rotation != value)
                {
                    _rotation = value;
                    RotationChanged.Raise(this, EventArgs.Empty);
                }
            }
        }

        [JsonIgnore]
        public EventHandler RotationChanged;

        private Vector2 _scale;
        public Vector2 Scale
        {
            get { return _scale; }
            set
            {
                if (_scale != value)
                {
                    _scale = value;
                    ScaleChanged.Raise(this, EventArgs.Empty);
                }
            }
        }

        [JsonIgnore]
        public EventHandler ScaleChanged;

        public void Attach(Type componentType, Component component)
        {
            if (component.Entity != null)
                throw new InvalidOperationException(string.Format("Component {0} is already attached to entity {1}", component, component.Entity));

            component.Entity = this;
            _components.Add(component);

            foreach (var system in _space.GetSystemsForType(componentType))
                system.Attach(component);
        }

        public void Attach<T>(T component) where  T : Component
        {
            var type = component.GetType();

            Attach(type, component);
        }

        public void Detach(Component component)
        {
            if (component.Entity != this)
                throw new InvalidOperationException(string.Format("Component {0} is not attached to entity {1}", component, this));
            
            component.Entity = null;
            _components.Remove(component);

            var componentType = component.GetType();

            foreach(var system in _space.GetSystemsForType(componentType))
                system.Detach(component);
        }

        public T GetComponent<T>(string name) where T : Component
        {
            return (T)_components.FirstOrDefault(i => i.Name == name && i is T);
        }

        public T GetComponent<T>() where T : Component
        {
            return (T)_components.FirstOrDefault(i => i is T);
        }

        public IEnumerable<T> GetComponents<T>() where T : Component
        {
            return _components.Where(i => i is T).Cast<T>();
        }

        public void Destory()
        {
            foreach (var component in _components.ToArray())
                Detach(component);

            _components.Clear();
        }
    }
}