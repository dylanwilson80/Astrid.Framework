using System.Collections.Generic;
using Astrid.Core;
using Astrid.Framework;
using Astrid.Framework.Assets;
using Astrid.Framework.Graphics;
using Astrid.Framework.Input;
using Astrid.Framework.Screens;
using AstridDemo.Screens;

namespace AstridDemo
{
    public class DemoGame : GameBase, ITouchInputListener
    {
        private List<Screen> _screens;
        private int _currentScreenIndex;

        private SpriteBatch _spriteBatch;
        private Texture _backgroundTexture;

        public DemoGame(ApplicationBase application)
            : base(application)
        {
        }

        public override void Create()
        {
            _screens = new List<Screen>
            {
                new GuiScreen(this),
                new AnimationScreen(this),
                new BitmapFontsScreen(this),
                new GdxTextureAtlasScreen(this)
            };

            SetScreen(_screens[_currentScreenIndex]);

            InputDevice.Processors.Add(new TouchInputProcessor(this));

            _backgroundTexture = AssetManager.Load<Texture>("hills_800x480.png");
            _spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        public override void Destroy()
        {
        }

        public override void Render(float deltaTime)
        {
            _spriteBatch.Begin();
            _spriteBatch.Draw(_backgroundTexture, Vector2.Zero);
            _spriteBatch.End();

            base.Render(deltaTime);
        }

        public bool OnTouchDown(Vector2 position, int pointerIndex)
        {
            return false;
        }

        public bool OnTouchUp(Vector2 position, int pointerIndex)
        {
            _currentScreenIndex++;

            if (_currentScreenIndex == _screens.Count)
                _currentScreenIndex = 0;

            SetScreen(_screens[_currentScreenIndex]);
            return true;
        }

        public bool OnTouchDrag(Vector2 position, Vector2 delta, int pointerIndex)
        {
            return false;
        }
    }
}
