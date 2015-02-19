using Astrid.Framework;
using Astrid.Framework.Screens;
using AstridDemo.Screens;

namespace AstridDemo
{
    public class DemoGame : GameBase
    {
        private ScreenManager _screenManager;

        public DemoGame(ApplicationBase application)
            : base(application)
        {
        }

        public override void Create()
        {
            _screenManager = new ScreenManager();
            _screenManager.SetScreen(new AnimationScreen(this));
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
    }
}
