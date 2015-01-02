using System;
using System.Collections.Generic;
using System.Linq;
using Astrid.Framework.Entities.Components;
using Newtonsoft.Json;

namespace Astrid.Framework.Serializers
{
    public class JsonKnownComponentTypesSerializationBinder : SerializationBinder
    {
        private readonly Dictionary<string, Type> _componentTypes;

        public JsonKnownComponentTypesSerializationBinder()
        {
            _componentTypes = typeof(Component)
                .Assembly
                .GetTypes()
                .Where(i => i.IsSubclassOf(typeof(Component)) && !i.IsAbstract)
                .ToDictionary(i => i.Name);
        }

        public override Type BindToType(string assemblyName, string typeName)
        {
            Type type;

            if(_componentTypes.TryGetValue(typeName, out type))
                return type;

            throw new InvalidOperationException(string.Format("Component type {0} not found", typeName));
        }

        public override void BindToName(Type serializedType, out string assemblyName, out string typeName)
        {
            assemblyName = null;
            typeName = serializedType.Name;
        }
    }
}