using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Astrid.Core;
using Astrid.Framework;
using Astrid.Framework.Animations;
using Astrid.Framework.Assets;
using Astrid.Framework.Graphics;
using Astrid.Framework.Input;
using Astrid.Framework.Screens;

namespace AstridDemo.Screens
{
    public class AnimationScreen : Screen, ITouchInputListener
    {
        private AnimationSystem _animationSystem;
        private SpriteBatch _spriteBatch;
        private Texture _texture;
        private Vector2 _position;
        private Color _color;

        public AnimationScreen(GameBase game) : base(game)
        {
        }

        public override void Resize(int width, int height)
        {
        }

        public override void Show()
        {
            _animationSystem = new AnimationSystem();

            InputDevice.Processors.Add(new TouchInputProcessor(this));

            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _texture = AssetManager.Load<Texture>("AstridLogo.png");

            var x = GraphicsDevice.Width / 2;
            var y = GraphicsDevice.Height / 2;
            _position = new Vector2(x, y);
            _color = Color.White;
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

        public override void Update(float deltaTime)
        {
            _animationSystem.Update(deltaTime);
        }

        public override void Render(float deltaTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            _spriteBatch.Draw(_texture, _position, _color, new Vector2(0.5f, 0.5f), 0, Vector2.One);
            _spriteBatch.End();
        }

        public override void Dispose()
        {
        }

        public bool OnTouchDown(Vector2 position, int pointerIndex)
        {
            _animationSystem.Attach(new ColorAnimation(_color, new Color(Color.Black, 0.0f), v => _color = v, 0.5f));
            return true;
        }

        public bool OnTouchUp(Vector2 position, int pointerIndex)
        {
            _animationSystem.Attach(new Vector2Animation(_position, position, v => _position = v, 1.2f) { EasingFunction = EasingFunctions.QuadraticEaseInOut });
            _animationSystem.Attach(new ColorAnimation(_color, Color.White, v => _color = v, 2.0f) { EasingFunction = EasingFunctions.QuadraticEaseInOut });
            return true;
        }

        public bool OnTouchDrag(Vector2 position, Vector2 delta, int pointerIndex)
        {
            return false;
        }
    }
}
