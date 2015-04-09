using System.Collections.Generic;
using Astrid;
using Astrid.Core;
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

            _clickSoundEffect = AssetManager.Load<SoundEffect>("click.wav");
        }

        private SoundEffect _clickSoundEffect;
        private SoundEffectInstance _soundEffectInstance;

        public override void Destroy()
        {
            _clickSoundEffect.Dispose();
        }

        public bool OnTouchDown(Vector2 position, int pointerIndex)
        {
            _soundEffectInstance = _clickSoundEffect.Play();
            return true;
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
