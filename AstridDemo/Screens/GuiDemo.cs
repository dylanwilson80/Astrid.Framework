using Astrid;
using Astrid.Core;
using Astrid.Gui;
using Astrid.Gui.Fonts;

namespace AstridDemo.Screens
{
    public class GuiDemo : Screen
    {
        public GuiDemo(GameBase game) 
            : base(game)
        {
        }

        public override void Show()
        {
            base.Show();

            var guiLayer = new GuiLayer(Viewport);
            Layers.Add(guiLayer);

            var guiButtonRegion = AssetManager.Load<Texture>("PlayButton.png").ToTextureRegion();
            var guiButton = new GuiButton(guiButtonRegion)
            {
                Position = new Vector2(400, 240)
            };
            guiLayer.Controls.Add(guiButton);

            var font = AssetManager.Load("CourierNew_32.fnt", new BitmapFontLoader());
            var guiLabel = new GuiLabel(font)
            {
                Text = "This is a label",
                HorizontalAlignment = HorizontalAlignment.Left,
                Position = new Vector2(100, 100),
                TextColor = Color.Black
            };
            guiLayer.Controls.Add(guiLabel);

            var guiImageTexture = AssetManager.Load<Texture>("AstridLogo.png");
            var guiImage = new GuiImage(guiImageTexture)
            {
                Position = new Vector2(400, 300)
            };
            guiLayer.Controls.Add(guiImage);
        }
    }
}