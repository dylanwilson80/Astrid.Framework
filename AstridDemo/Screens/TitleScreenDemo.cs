using System;
using Astrid.Core;
using Astrid.Framework;
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
                Position = new Vector2(400, 100)
            };
            guiLayer.Controls.Add(image);

            var buttonTexture = AssetManager.Load<Texture>("PlayButton.png");
            var normalSprite = new Sprite(buttonTexture);
            var pressedSprite = new Sprite(buttonTexture) {Color = Color.Gray};
            var playButton = new GuiButton(normalSprite, pressedSprite)
            {
                Position = new Vector2(400, 260)
            };
            playButton.Click += PlayButtonOnClick;
            guiLayer.Controls.Add(playButton);
            Layers.Add(guiLayer);

            base.Show();
        }

        private void PlayButtonOnClick(object sender, EventArgs eventArgs)
        {
            SetScreen(new AnimationDemo(Game));
        }
    }
}