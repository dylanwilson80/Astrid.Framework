using System;
using Astrid.Framework.Entities.Components;

namespace Astrid.Framework.Entities.Systems
{
    public abstract class ComponentSystem
    {
        internal abstract void Attach(Component component);
        internal abstract void Detach(Component component);
        public abstract Type ComponentType { get; }
        public abstract void Update(float deltaTime);
    }

    public abstract class ComponentSystem<T> : ComponentSystem
        where T : Component
    {
        internal override void Attach(Component component)
        {
            OnAttached((T) component);
        }

        internal override void Detach(Component component)
        {
            OnDetached((T) component);
        }

        public override Type ComponentType
        {
            get { return typeof (T); }
        }

        protected abstract void OnAttached(T component);
        protected abstract void OnDetached(T component);

        public override string ToString()
        {
            return string.Format("{0}<{1}>", GetType().Name, ComponentType.Name);
        }
    }
}