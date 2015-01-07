using System;
using Astrid.Core;
using Astrid.Framework;
using Astrid.Framework.Assets;
using Astrid.Framework.Entities;
using Astrid.Framework.Entities.Components;
using Astrid.Framework.Entities.Components.Gui;
using Astrid.Framework.Graphics;

namespace AstridDemo
{
    public interface IPlatformService
    {
        void OpenUrl(string url);
    }

    public class DemoGame : GameBase
    {
        public DemoGame(ApplicationBase application, IPlatformService platformService)
            : base(application)
        {
            _platformService = platformService;
        }

        private readonly IPlatformService _platformService;
        private EntityEngine _engine;

        public override void Create()
        {
            var camera = new Camera { Origin = Vector2.Zero, Position = Vector2.Zero, Rotation = 0, Zoom = 1 };
            var systemFactory = new DefaultComponentSystemFactory(this, camera);

            _engine = new EntityEngine(AssetManager, systemFactory);
            
            var scene = _engine.LoadScene("Scene1.scene");
            var guiLayer = scene.GetLayer("GuiLayer");

            var button = guiLayer.GetComponent<GuiButton>("PlayButton");
            button.IsEnabled = true;
            button.Click += PlayButton_Click;

            var toggle = guiLayer.GetComponent<GuiToggleButton>("SoundToggle");
            toggle.CheckChanged += ToggleOnCheckChanged;
        }

        private void ToggleOnCheckChanged(object sender, EventArgs eventArgs)
        {
        }

        private void PlayButton_Click(object sender, EventArgs eventArgs)
        {
            _platformService.OpenUrl("www.craftworkgames.com");
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
            //InputDevice.Update();
            _engine.Update(deltaTime);
        }

        public override void Render(float deltaTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _engine.Draw(deltaTime);
        }
    }
}
