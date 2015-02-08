using Astrid.Core;
using Astrid.Framework;
using Astrid.Framework.Assets;
using Astrid.Framework.Audio;
using Astrid.Framework.Graphics;
using Astrid.Framework.Input;

namespace AstridDemo
{
    public class DemoGame : GameBase, ITouchInputListener
    {
        private SpriteBatch _spriteBatch;
        private Texture _texture;
        private Vector2 _position;
        private SoundEffect _soundEffect;

        public DemoGame(ApplicationBase application)
            : base(application)
        {
        }

        public override void Create()
        {
            InputDevice.Processors.Add(new TouchInputProcessor(this));

            _soundEffect = AssetManager.Load<SoundEffect>(@"song.mp3");

            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _texture = AssetManager.Load<Texture>("AstridLogo.png");

            var x = GraphicsDevice.Width / 2;
            var y = GraphicsDevice.Height / 2;
            _position = new Vector2(x, y);
        }

        public override void Destroy()
        {
            _soundEffect.Dispose();
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

        private float _rotation;
        private bool _isRotating;

        public override void Update(float deltaTime)
        {
            if(_isRotating)
                _rotation += deltaTime * 8;
        }

        public override void Render(float deltaTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            _spriteBatch.Draw(_texture, _position, Color.White, new Vector2(0.5f, 0.5f), _rotation, Vector2.One);
            _spriteBatch.End();
        }

        public bool OnTouchDown(Vector2 position, int pointerIndex)
        {
            _isRotating = true;
            return true;
        }

        public bool OnTouchUp(Vector2 position, int pointerIndex)
        {
            _soundEffect.Play();
            _isRotating = false;
            return true;
        }

        public bool OnTouchDrag(Vector2 position, Vector2 delta, int pointerIndex)
        {
            _position += delta;
            return true;
        }
    }
}
