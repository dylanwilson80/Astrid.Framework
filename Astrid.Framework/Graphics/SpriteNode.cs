using Astrid.Core;

namespace Astrid.Framework.Graphics
{
    public class SpriteNode : SceneNode<Sprite>
    {
        public SpriteNode(string name, Sprite sprite, Vector2 position)
            : base(name, sprite, position)
        {
        }

        public SpriteNode(Sprite sprite, Vector2 position)
            : base(sprite, position)
        {
        }
    }
}