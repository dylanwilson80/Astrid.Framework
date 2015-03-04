using Astrid.Core;
using Astrid.Framework;
using Astrid.Framework.Assets;
using Astrid.Framework.Entities;
using Astrid.Framework.Entities.Components;
using Astrid.Framework.Graphics;
using Astrid.Framework.Screens;

namespace AstridDemo.Screens
{
    public class GuiScreen : Screen
    {
        public GuiScreen(GameBase game) 
            : base(game)
        {
        }

        private EntityEngine _engine;

        public override void Load()
        {
            base.Load();


            
            var camera = new Camera();
            _engine = new EntityEngine(AssetManager, new ComponentSystemFactory(this, camera));

            //var space = _engine.CreateSpace("GuiSpace");
            //var entity = space.CreateEntity(new Vector2(400, 240));
            var playTexture = AssetManager.Load<Texture>("PlayButton.png");
            var textureRegion = new TextureRegion(playTexture);
            //entity.Attach(new Sprite(textureRegion));
            //entity.Attach(new GuiButton());

            var spriteLayer = new SpriteLayer(GraphicsDevice);
            var sprite = new Sprite(textureRegion);
            var node = new SpriteNode(sprite, new Vector2(400, 240));
            spriteLayer.Nodes.Add(node);
            Layers.Add(spriteLayer);
        }

        public override void Update(float deltaTime)
        {
            //_engine.Update(deltaTime);
        }

        public override void Render(float deltaTime)
        {
            //GraphicsDevice.Clear(Color.CornflowerBlue);
            //_engine.Draw(deltaTime);

            base.Render(deltaTime);
        }
    }
}