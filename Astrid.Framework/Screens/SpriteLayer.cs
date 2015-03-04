using System.Collections.Generic;
using Astrid.Framework.Graphics;

namespace Astrid.Framework.Screens
{
    public class SpriteLayer : ScreenLayer
    {
        public SpriteLayer(GraphicsDevice graphicsDevice)
        {
            _sprites = new List<Sprite>();
            _spriteBatch = new SpriteBatch(graphicsDevice);
        }

        private readonly SpriteBatch _spriteBatch;
        private readonly List<Sprite> _sprites;

        public IList<Sprite> Sprites
        {
            get { return _sprites; }
        }

        public override void Render(float deltaTime)
        {
            _spriteBatch.Begin();

            foreach (var sprite in _sprites)
                _spriteBatch.Draw(sprite);

            _spriteBatch.End();
        }
    }
}