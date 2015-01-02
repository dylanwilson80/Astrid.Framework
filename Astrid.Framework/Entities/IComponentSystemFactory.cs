using System;
using Astrid.Framework.Entities.Systems;

namespace Astrid.Framework.Entities
{
    public interface IComponentSystemFactory
    {
        ComponentSystem CreateSystemForType(Type type);
    }
}