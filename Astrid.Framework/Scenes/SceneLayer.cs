using System.Collections.Generic;
using System.Linq;
using Astrid.Framework.Entities.Components;

namespace Astrid.Framework.Scenes
{
    public class SceneLayer
    {
        public SceneLayer()
            : this (string.Empty)
        {
        }

        public SceneLayer(string name)
        {
            Name = name;
            Nodes = new List<SceneNode>();
        }

        public string Name { get; set; }
        public List<SceneNode> Nodes { get; private set; }

        public T GetComponent<T>(string name)
            where T : Component
        {
            var components = Nodes.SelectMany(i => i.Components);
            var component = components.FirstOrDefault(i => i.Name == name);
            return component as T;
        }
    }
}