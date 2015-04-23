using Astrid;
using Astrid.Core;
using Astrid.Gui;
using Astrid.Gui.Fonts;

namespace AstridDemo.Screens
{
    public class BitmapFontDemo : Screen
    {
        public BitmapFontDemo(GameBase game)
            : base(game)
        {
        }

        private SpriteBatch _spriteBatch;
        private BitmapFont _bitmapFont;

        public override void Show()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _bitmapFont = AssetManager.Load("courier-new-32.fnt", new BitmapFontLoader());
        }

        private Vector2 _position;

        public override void Update(float deltaTime)
        {
            _position = InputDevice.Position;
        }

        public override void Render(float deltaTime)
        {
            base.Render(deltaTime);

            _spriteBatch.Begin(Game.Viewport.Camera.GetViewMatrix());
            _bitmapFont.Draw(_spriteBatch, string.Format("Position: {0:0.0}, {1:0.0}", _position.X, _position.Y), 2, 2, Color.White);
            _bitmapFont.Draw(_spriteBatch, "This is\nMultiline text", 100, 100, Color.Red);
            _bitmapFont.Draw(_spriteBatch, "This text is wrapped to a fixed width of two hundred and fifty pixels.", 200, 300, 250, Color.Green);
            _spriteBatch.End();
        }
    }
}
