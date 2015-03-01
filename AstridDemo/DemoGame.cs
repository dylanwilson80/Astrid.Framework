using System.Collections.Generic;
using Astrid.Core;
using Astrid.Framework;
using Astrid.Framework.Input;
using Astrid.Framework.Screens;
using AstridDemo.Screens;

namespace AstridDemo
{
    public class DemoGame : GameBase, ITouchInputListener
    {
        private ScreenManager _screenManager;
        private List<Screen> _screens;
        private int _currentScreenIndex;

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

            _screenManager = new ScreenManager();
            _screenManager.SetScreen(_screens[_currentScreenIndex]);

            InputDevice.Processors.Add(new TouchInputProcessor(this));
        }

        public override void Destroy()
        {
        }

        public override void Pause()
        {
            _screenManager.Pause();
        }

        public override void Resume()
        {
            _screenManager.Resume();
        }

        public override void Resize(int width, int height)
        {
            _screenManager.Resize(width, height);
        }

        public override void Update(float deltaTime)
        {
            _screenManager.Update(deltaTime);
        }

        public override void Render(float deltaTime)
        {
            _screenManager.Render(deltaTime);
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

            _screenManager.SetScreen(_screens[_currentScreenIndex]);
            return true;
        }

        public bool OnTouchDrag(Vector2 position, Vector2 delta, int pointerIndex)
        {
            return false;
        }
    }
}
