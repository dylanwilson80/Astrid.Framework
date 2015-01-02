using System.Collections.Generic;
using Astrid.Core;
using Astrid.Framework.Entities.Components;

namespace Astrid.Framework.Scenes
{
    public class SceneNode
    {
        public SceneNode()
        {
            Components = new List<Component>();
        }

        public string Name { get; set; }
        public Vector2 Position { get; set; }
        public float Rotation { get; set; }
        public Vector2 Scale { get; set; }
        public List<Component> Components { get; set; }

        public override string ToString()
        {
            return string.Format("Name: {0} Position: {1} Rotation: {2} Scale: {3}", Name, Position, Rotation, Scale);
        }
    }
}