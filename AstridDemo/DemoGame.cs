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

            _soundEffect = AssetManager.Load<SoundEffect>("click.wav");
            _music = AssetManager.Load<Music>("song.mp3");
            _music.Play();
            _music.Volume = 0.10f;
        }

        public override void Pause()
        {
            if(_music != null)
                _music.Pause();

            base.Pause();
        }

        public override void Resume()
        {
            if(_music != null)
                _music.Resume();

            base.Resume();
        }

        private Music _music;
        private SoundEffect _soundEffect;

        public override void Destroy()
        {
            _soundEffect.Dispose();
            _music.Dispose();
        }

        public bool OnTouchDown(Vector2 position, int pointerIndex)
        {
            if (_music.IsPlaying)
                _music.Pause();
            else
                _music.Resume();

            _soundEffect.Play();
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
