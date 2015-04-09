using Astrid;
using Astrid.Gui;

namespace AstridDemo.Screens
{
    public class GuiDemo : Screen
    {
        public GuiDemo(GameBase screenManager) 
            : base(screenManager)
        {
        }

        public override void Show()
        {
            base.Show();
            
            //var camera = new Camera();
            //_engine = new EntityEngine(AssetManager, new ComponentSystemFactory(this, camera));

            var playTexture = AssetManager.Load<Texture>("PlayButton.png");
            var textureRegion = new TextureRegion(playTexture);
            var spriteLayer = new SpriteLayer(Viewport);
            var sprite0 = new Sprite(textureRegion);//, new Vector2(400, 240));
            var sprite1 = new Sprite(textureRegion);//, new Vector2(500, 140));
            spriteLayer.Sprites.Add(sprite0);
            spriteLayer.Sprites.Add(sprite1);
            Layers.Add(spriteLayer);
        }
    }
}