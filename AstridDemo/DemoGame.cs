using Astrid.Core;
using Astrid.Framework;
using Astrid.Framework.Assets;
using Astrid.Framework.Graphics;

namespace AstridDemo
{
    public class DemoGame : GameBase
    {
        private SpriteBatch _spriteBatch;
        private Texture _texture;

        public DemoGame(ApplicationBase application)
            : base(application)
        {
        }

        public override void Create()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _texture = AssetManager.Load<Texture>("AstridLogo.png");
        }

        public override void Destroy()
        {
        }

        public override void Pause()
        {
        }

        public override void Resume()
        {
        }

        public override void Resize(int width, int height)
        {
        }

        public override void Update(float deltaTime)
        {
        }

        public override void Render(float deltaTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            _spriteBatch.Draw(_texture, Vector2.Zero);
            _spriteBatch.End();
        }
    }
}
