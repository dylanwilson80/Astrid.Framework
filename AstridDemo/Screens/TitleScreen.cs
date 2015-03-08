using System;
using Astrid.Core;
using Astrid.Framework;
using Astrid.Framework.Assets;
using Astrid.Framework.Entities.Components.Gui;
using Astrid.Framework.Graphics;
using Astrid.Framework.Screens;

namespace AstridDemo.Screens
{
    public class TitleScreen : Screen
    {
        public TitleScreen(GameBase game) 
            : base(game)
        {
        }

        public override void Show()
        {
            var guiLayer = new GuiLayer(GraphicsDevice);

            var normalSprite = AssetManager.Load<Texture>("PlayButton.png").ToSprite();
            var pressedSprite = new Sprite(normalSprite.TextureRegion) {Color = Color.Gray};
            var button = new GuiButton(normalSprite, pressedSprite)
            {
                Position = new Vector2(400, 240)
            };
            button.Click += ButtonOnClick;
            guiLayer.Controls.Add(button);
            Layers.Add(guiLayer);

            base.Show();
        }

        private void ButtonOnClick(object sender, EventArgs eventArgs)
        {
            SetScreen(new AnimationDemo(Game));
        }
    }
}