using Astrid.Core;
using Astrid.Framework;
using Astrid.Framework.Animations;
using Astrid.Framework.Assets;
using Astrid.Framework.Graphics;
using Astrid.Framework.Screens;

namespace AstridDemo.Screens
{
    public class AnimationDemo : Screen
    {
        private AnimationSystem _animationSystem;
        private SpriteBatch _spriteBatch;
        private Texture _texture;
        private Vector2 _position;
        private Color _color;

        public AnimationDemo(GameBase game) : base(game)
        {
        }

        public override void Show()
        {
            _animationSystem = new AnimationSystem();

            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _texture = AssetManager.Load<Texture>("AstridLogo.png");

            var x = Viewport.Width / 2;
            var y = Viewport.Height / 2;
            _position = new Vector2(x, y);
            _color = Color.White;
            
            CreateMoveToAnimation(new Vector2(x - 100, y), new Vector2(x + 100, y));
        }

        private void CreateMoveToAnimation(Vector2 position0, Vector2 position1)
        {
            var animation = new Vector2Transition(position0, position1, v => _position = v, 1.2f)
            {
                EasingFunction = EasingFunctions.QuarticEaseInOut
            };
            animation.TransitionComplete += (sender, args) => CreateMoveToAnimation(position1, position0);
            _animationSystem.Attach(animation);
        }

        public override void Update(float deltaTime)
        {
            _animationSystem.Update(deltaTime);
        }

        public override void Render(float deltaTime)
        {
            base.Render(deltaTime);

            _spriteBatch.Begin(Viewport.Camera.GetViewMatrix());
            _spriteBatch.Draw(_texture, _position, _color, new Vector2(0.5f, 0.5f), 0, Vector2.One);
            _spriteBatch.End();
        }
    }
}
