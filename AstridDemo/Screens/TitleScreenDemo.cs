using System;
using Astrid.Core;
using Astrid.Framework;
using Astrid.Framework.Animations;
using Astrid.Framework.Assets;
using Astrid.Framework.Assets.Fonts;
using Astrid.Framework.Graphics;
using Astrid.Framework.Gui;
using Astrid.Framework.Screens;

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
                Position = new Vector2(400, 100),
                
            };
            guiLayer.Controls.Add(image);

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

            const float duration = 1.5f;

            Animations
                .CreateSequence(playButton)
                .MoveTo(new Vector2(0, 0), duration)
                .MoveTo(new Vector2(800, 480), duration)
                .MoveTo(new Vector2(800, 260), duration)
                .MoveTo(new Vector2(400, 260), duration)
                .RotateTo(0, duration)
                .FadeOut(duration)
                .Play();
                

            base.Show();
        }

        private void PlayButtonOnClick(object sender, EventArgs eventArgs)
        {
            SetScreen(new AnimationDemo(Game));
        }
    }
}