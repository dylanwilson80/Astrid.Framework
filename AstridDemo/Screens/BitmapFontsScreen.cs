using Astrid.Core;
using Astrid.Framework;
using Astrid.Framework.Assets.Fonts;
using Astrid.Framework.Graphics;
using Astrid.Framework.Screens;

namespace AstridDemo.Screens
{
    public class BitmapFontsScreen : Screen
    {
        public BitmapFontsScreen(GameBase game) 
            : base(game)
        {
        }

        private SpriteBatch _spriteBatch;
        private BitmapFont _bitmapFont;

        public override void Resize(int width, int height)
        {
        }

        public override void Show()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _bitmapFont = AssetManager.Load("CourierNew_32.fnt", new BitmapFontLoader());
        }

        public override void Hide()
        {
        }

        public override void Pause()
        {
        }

        public override void Resume()
        {
        }

        private Vector2 _position;

        public override void Update(float deltaTime)
        {
            _position = InputDevice.Position;
        }

        public override void Render(float deltaTime)
        {
            //GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            _bitmapFont.Draw(_spriteBatch, string.Format("Position: {0:0.0}, {1:0.0}", _position.X, _position.Y), 2, 2, Color.White);
            _bitmapFont.Draw(_spriteBatch, "This is\nMultiline text", 100, 100, Color.Red);
            _bitmapFont.Draw(_spriteBatch, "This text is wrapped to a fixed width of two hundred and fifty pixels.", 200, 300, 250, Color.Green);
            _spriteBatch.End();
        }

        public override void Dispose()
        {
        }
    }
}
