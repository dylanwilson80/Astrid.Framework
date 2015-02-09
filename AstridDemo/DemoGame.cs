using Astrid.Core;
using Astrid.Framework;
using Astrid.Framework.Animations;
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
        private AnimationSystem _animationSystem;

        public DemoGame(ApplicationBase application)
            : base(application)
        {
        }

        public override void Create()
        {
            _animationSystem = new AnimationSystem();

            InputDevice.Processors.Add(new TouchInputProcessor(this));

            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _texture = AssetManager.Load<Texture>("AstridLogo.png");

            var x = GraphicsDevice.Width / 2;
            var y = GraphicsDevice.Height / 2;
            _position = new Vector2(x, y);
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
            _animationSystem.Update(deltaTime);
        }

        public override void Render(float deltaTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            
            _spriteBatch.Begin();
            _spriteBatch.Draw(_texture, _position, Color.White, new Vector2(0.5f, 0.5f), 0, Vector2.One);
            _spriteBatch.End();
        }

        public bool OnTouchDown(Vector2 position, int pointerIndex)
        {
            return true;
        }

        public bool OnTouchUp(Vector2 position, int pointerIndex)
        {
            _animationSystem.Attach(new FloatAnimation(_position.X, position.X, v => _position.X = v, 1.2f)
            {
                EasingFunction = EasingFunctions.QuadraticEaseInOut
            });
            _animationSystem.Attach(new FloatAnimation(_position.Y, position.Y, v => _position.Y = v, 1.5f)
            {
                EasingFunction = EasingFunctions.QuadraticEaseInOut
            });
            return true;
        }

        public bool OnTouchDrag(Vector2 position, Vector2 delta, int pointerIndex)
        {
            return true;
        }
    }
}
