using System;
using Astrid.Animations;
using Astrid.Core;
using Astrid.Framework;
using Astrid.Framework.Assets;
using Astrid.Framework.Assets.Fonts;
using Astrid.Gui;

namespace AstridDemo.Screens
{
    public class TitleScreenDemo : Screen
    {
        public TitleScreenDemo(GameBase game) 
            : base(game)
        {
        }

        public override void Show()
        {
            var guiLayer = new GuiLayer(GraphicsDevice);

            var font = AssetManager.Load("CourierNew_32.fnt", new BitmapFontLoader());
            var label = new GuiLabel(font, null)
            {
                Text = "Welcome to", 
                TextColor = new Color(81, 32, 0),
                Position = new Vector2(400, 45)
            };
            guiLayer.Controls.Add(label);

            var logoTexture = AssetManager.Load<Texture>("AstridLogo.png");
            var image = new GuiImage(logoTexture)
            {
                Position = new Vector2(400, -100),
            };
            guiLayer.Controls.Add(image);


            var parameters = new TransitionParameters(1.0f, EasingFunctions.QuadraticEaseIn);
            Animations.CreateSequence(image)
                .MoveTo(new Vector2(400, 100), parameters)
                .ScaleTo(new Vector2(1.0f, 0.8f), new TransitionParameters(0.6f, EasingFunctions.CubicEaseInOut))
                .ScaleTo(new Vector2(1.0f, 1.0f), new TransitionParameters(0.6f, EasingFunctions.CubicEaseInOut))
                .Play();

            var buttonTexture = AssetManager.Load<Texture>("PlayButton.png");
            var normalSprite = new Sprite(buttonTexture);
            var pressedSprite = new Sprite(buttonTexture) {Color = Color.Gray};
            var playButton = new GuiButton(normalSprite, pressedSprite)
            {
                Position = new Vector2(400, -260),
                Rotation = MathHelper.TwoPi
            };
            playButton.Click += PlayButtonOnClick;
            guiLayer.Controls.Add(playButton);
            Layers.Add(guiLayer);

            var transitionParameters = new TransitionParameters(1.0f, EasingFunctions.CubicEaseInOut);
            Animations
                .CreateSequence(playButton)
                .MoveTo(new Vector2(50, 50), transitionParameters)
                .MoveTo(new Vector2(700, 400), transitionParameters)
                .MoveTo(new Vector2(700, 230), transitionParameters)
                .MoveTo(new Vector2(400, 250), transitionParameters)
                .Delay(1.0f)
                .RotateTo(0, transitionParameters)
                .Delay(1.0f)
                .RotateTo(MathHelper.TwoPi, transitionParameters)
                .FadeOut(transitionParameters)
                .FadeIn(transitionParameters)
                .Play();
            
            base.Show();
        }

        private void PlayButtonOnClick(object sender, EventArgs eventArgs)
        {
            SetScreen(new AnimationDemo(Game));
        }
    }
}