using Astrid;
using Astrid.Animations;
using Astrid.Core;
using Astrid.Gui;

namespace AstridDemo.Screens
{
    public class SplashDemo : Screen
    {
        private readonly GameBase _game;

        public SplashDemo(GameBase game) 
            : base(game)
        {
            _game = game;
        }

        public override void Show()
        {
            var spriteLayer = new SpriteLayer(GraphicsDevice, Game.Camera);

            ClearColor = Color.Black;

            var sprite = AssetManager
                .Load<Texture>("astrid-logo.png")
                .ToSprite();
            sprite.Color = new Color(Color.White, 0.0f);
            sprite.Position = new Vector2(400, 240);
            spriteLayer.Sprites.Add(sprite);
            Layers.Add(spriteLayer);

            Animations
                .CreateSequence(sprite)
                .FadeIn(new TransitionParameters(0.5f, EasingFunctions.CubicEaseInOut))
                .Delay(1.0f)
                .FadeOut(new TransitionParameters(0.5f, EasingFunctions.CubicEaseInOut))
                .Execute(() => SetScreen(new TitleScreenDemo(_game)))
                .Play();
                
            base.Show();
        }
    }
}