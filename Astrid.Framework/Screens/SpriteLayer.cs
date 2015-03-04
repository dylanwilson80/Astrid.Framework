using System.Collections.Generic;
using Astrid.Framework.Graphics;

namespace Astrid.Framework.Screens
{
    public class SpriteLayer : ScreenLayer
    {
        public SpriteLayer(GraphicsDevice graphicsDevice)
        {
            _nodes = new List<SpriteNode>();
            _spriteBatch = new SpriteBatch(graphicsDevice);
        }

        private readonly SpriteBatch _spriteBatch;
        private readonly List<SpriteNode> _nodes;

        public IList<SpriteNode> Nodes
        {
            get { return _nodes; }
        }

        public override void Render(float deltaTime)
        {
            _spriteBatch.Begin();

            foreach (var node in _nodes)
                _spriteBatch.Draw(node.Node, node.Position, node.Rotation, node.Scale);

            _spriteBatch.End();
        }
    }
}