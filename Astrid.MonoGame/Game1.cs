using System;
using Astrid.Framework.Assets;
using Astrid.Framework.Entities;
using Astrid.Framework.Entities.Components;
using Astrid.Framework.Entities.Components.Gui;
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

            var componentSystemFactory = new MonoGameComponentSystemFactory(GraphicsDevice);
            _engine = new EntityEngine(assetManager, componentSystemFactory);
            
            // create background
            // TODO: This could be loaded from the scene file
            var backgroundSpace = _engine.CreateSpace("BackgroundSpace");
            var backgroundEntity = backgroundSpace.CreateEntity();
            var textureRegion = assetManager.Load<TextureRegion>("hills_800x480.png");
            var sprite = new Sprite(textureRegion) { Origin = new Astrid.Core.Vector2() };
            backgroundEntity.Attach(sprite);

            // Load scene file and wire up button event handlers
            var scene = _engine.LoadScene("Scene1.scene");
            
            var button = scene.GetComponent<GuiButton>("PlayButton");
            button.IsEnabled = true;
            button.Pressed += PlayButton_Pressed;

            var toggle = scene.GetComponent<GuiToggleButton>("SoundToggle");
            toggle.CheckChanged += ToggleOnCheckChanged;
        }

        private void ToggleOnCheckChanged(object sender, EventArgs e)
        {
        }

        private void PlayButton_Pressed(object sender, EventArgs e)
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
