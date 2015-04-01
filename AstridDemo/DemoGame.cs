using System.Collections.Generic;
using Astrid.Core;
using Astrid.Engine;
using Astrid.Framework;
using Astrid.Gui;
using AstridDemo.Screens;

namespace AstridDemo
{
    public class DemoGame : GameBase, ITouchInputListener
    {
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
                new TitleScreenDemo(this),
                new SplashDemo(this),
                new GuiDemo(this),
                new AnimationDemo(this),
                new BitmapFontDemo(this),
                new GdxTextureAtlasDemo(this)
            };

            SetScreen(_screens[_currentScreenIndex]);
            InputDevice.Processors.Add(new TouchInputProcessor(this));
        }

        public override void Destroy()
        {
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

            //SetScreen(_screens[_currentScreenIndex]);
            return true;
        }

        public bool OnTouchDrag(Vector2 position, Vector2 delta, int pointerIndex)
        {
            return false;
        }
    }
}
