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
                new GuiDemo(this),
                new SplashDemo(this),
                new TitleScreenDemo(this),
                new AnimationDemo(this),
                new BitmapFontDemo(this),
                new GdxTextureAtlasDemo(this)
            };

            SetScreen(_screens[_currentScreenIndex]);
            InputDevice.Processors.Add(new TouchInputProcessor(this));

            AudioDevice.IsMusicEnabled = false;

            _music = AssetManager.Load<Music>("song.mp3");
            _music.Volume = 0.1f;
            _music.Play();

            _soundEffect = AssetManager.Load<SoundEffect>("click.wav");
            _soundEffect.Play();
        }

        private SoundEffect _soundEffect;
        private Music _music;

        public override void Destroy()
        {
            _music.Dispose();
            _soundEffect.Dispose();
        }

        public bool OnTouchDown(Vector2 position, int pointerIndex)
        {
            _soundEffect.Play();
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
