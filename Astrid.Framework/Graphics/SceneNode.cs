using Astrid.Core;

namespace Astrid.Framework.Graphics
{
    public abstract class SceneNode
    {
        protected SceneNode()
            : this(string.Empty, Vector2.Zero)
        {
        }

        protected SceneNode(Vector2 position)
            : this(string.Empty, position)
        {
        }

        protected SceneNode(string name, Vector2 position)
        {
            Name = name;
            Position = position;
            Scale = Vector2.One;
        }

        public string Name { get; set; }
        public Vector2 Position { get; set; }
        public float Rotation { get; set; }
        public Vector2 Scale { get; set; }

        public override string ToString()
        {
            return string.Format("Name: {0} Position: {1} Rotation: {2} Scale: {3}", Name, Position, Rotation, Scale);
        }
    }
}