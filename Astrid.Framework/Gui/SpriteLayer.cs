using System.Collections.Generic;

namespace Astrid.Gui
{
    public class SpriteLayer : ScreenLayer
    {
        public SpriteLayer(Viewport viewport)
            : base(viewport)
        {
            _viewport = viewport;
            _sprites = new List<Sprite>();
            _spriteBatch = new SpriteBatch(viewport.GraphicsDevice);
        }

        private readonly Viewport _viewport;
        private readonly SpriteBatch _spriteBatch;
        private readonly List<Sprite> _sprites;

        public IList<Sprite> Sprites
        {
            get { return _sprites; }
        }

        public override void Render(float deltaTime)
        {
            var viewMatrix = _viewport.Camera.GetViewMatrix();
            _spriteBatch.Begin(viewMatrix);

            //foreach (var sprite in _sprites)
            //    _spriteBatch.Draw(sprite);

            _spriteBatch.End();
        }
    }
}