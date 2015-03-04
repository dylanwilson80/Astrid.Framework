using Astrid.Core;

namespace Astrid.Framework.Graphics
{
    public class SceneNode<T>
    {
        public SceneNode(T node)
            : this(string.Empty, node, Vector2.Zero)
        {
        }

        public SceneNode(T node, Vector2 position)
            : this(string.Empty, node, position)
        {
        }

        public SceneNode(string name, T node, Vector2 position)
        {
            Name = name;
            Node = node;
            Position = position;
            Scale = Vector2.One;
        }

        public string Name { get; set; }
        public Vector2 Position { get; set; }
        public float Rotation { get; set; }
        public Vector2 Scale { get; set; }
        public T Node { get; private set; }

        public override string ToString()
        {
            return string.Format("Name: {0} Position: {1} Rotation: {2} Scale: {3}", Name, Position, Rotation, Scale);
        }
    }
}