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
            
            // TODO: Load background from the scene file
            // create background
            var backgroundSpace = _engine.CreateSpace("BackgroundSpace");
            var backgroundEntity = backgroundSpace.CreateEntity();
            var textureRegion = AssetManager.Load<TextureRegion>("hills_800x480.png");
            var sprite = new Sprite(textureRegion) {Origin = Vector2.Zero};
            backgroundEntity.Attach(sprite);
            
            // Load scene file and wire up button event handlers
            var scene = _engine.LoadScene("Scene1.scene");

            var button = scene.GetComponent<GuiButton>("PlayButton");
            button.IsEnabled = true;
            button.Pressed += PlayButton_Pressed;

            var toggle = scene.GetComponent<GuiToggleButton>("SoundToggle");
            toggle.CheckChanged += ToggleOnCheckChanged;
        }

        private void ToggleOnCheckChanged(object sender, EventArgs eventArgs)
        {
        }

        private void PlayButton_Pressed(object sender, EventArgs eventArgs)
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
            InputDevice.Update();
            _engine.Update(deltaTime);
        }

        public override void Render(float deltaTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _engine.Draw(deltaTime);
        }
    }
}
