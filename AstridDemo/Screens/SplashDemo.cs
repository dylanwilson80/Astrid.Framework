using Astrid.Engine;
using Astrid.Framework;
using Astrid.Gui;

namespace AstridDemo.Screens
{
    public class SplashDemo : Screen
    {
        public SplashDemo(GameBase screenManager) 
            : base(screenManager)
        {
        }

        public override void Show()
        {
            var spriteLayer = new SpriteLayer(Viewport);
            var sprite = AssetManager
                .Load<Texture>("AstridLogo.png")
                .ToSprite();
            //sprite.Position = new Vector2(400, 240);
            spriteLayer.Sprites.Add(sprite);
            Layers.Add(spriteLayer);

            base.Show();
        }
    }
}