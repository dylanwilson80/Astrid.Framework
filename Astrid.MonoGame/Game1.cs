using System;
using Astrid.Framework.Assets;
using Astrid.Framework.Entities;
using Astrid.Framework.Entities.Components;
using Astrid.Framework.Entities.Components.Gui;
using Astrid.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Astrid.MonoGame
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphicsDeviceManager;
        private EntityEngine _engine;
        
        public Game1()
        {
            _graphicsDeviceManager = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void LoadContent()
        {
            var assetManager = new MonoGameAssetManager(Content);

            var camera = new Camera();
            var deviceManager = new MonoGameDeviceManager(GraphicsDevice, camera);
            var componentSystemFactory = new MonoGameComponentSystemFactory(deviceManager, camera);
            _engine = new EntityEngine(assetManager, componentSystemFactory);

            var scene = _engine.LoadScene("Scene1.scene");
            //var guiLayer = scene.GetLayer("GuiLayer");

            //var button = guiLayer.GetComponent<GuiButton>("PlayButton");
            //button.IsEnabled = true;
            //button.Click += PlayButton_Click;

            //var toggle = guiLayer.GetComponent<GuiToggleButton>("SoundToggle");
            //toggle.CheckChanged += ToggleOnCheckChanged;
        }

        private void ToggleOnCheckChanged(object sender, EventArgs e)
        {
        }

        private void PlayButton_Click(object sender, EventArgs e)
        {
        }

        protected override void Update(GameTime gameTime)
        {
            var deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            _engine.Update(deltaTime);

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            var deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            //GraphicsDevice.Clear(Color.CornflowerBlue);

            _engine.Draw(deltaTime);

            base.Draw(gameTime);
        }
    }
}
