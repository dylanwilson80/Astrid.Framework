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

        private IMovable _mouseCursor;

        public override void Show()
        {
            base.Show();

            var guiLayer = new GuiLayer(GraphicsDevice, Game.Camera);
            Layers.Add(guiLayer);

            var guiButtonRegion = AssetManager.Load<Texture>("play-button.png").ToTextureRegion();
            var guiButton = new GuiButton(guiButtonRegion)
            {
                Position = new Vector2(400, 240)
            };
            guiLayer.Controls.Add(guiButton);
            _mouseCursor = guiButton;

            var font = AssetManager.Load("courier-new-32.fnt", new BitmapFontLoader());
            var guiLabel = new GuiLabel(font)
            {
                Text = "This is a label",
                HorizontalAlignment = HorizontalAlignment.Left,
                Position = new Vector2(100, 100),
                TextColor = Color.Black
            };
            guiLayer.Controls.Add(guiLabel);

            var guiImageTexture = AssetManager.Load<Texture>("photo.png");
            var guiImage = new GuiImage(guiImageTexture)
            {
                Position = new Vector2(800, 0),
                Origin = new Vector2(1.0f, 0.0f)
            };
            guiLayer.Controls.Add(guiImage);

            var guiCheckboxTexture = AssetManager.Load<Texture>("play-button.png");
            var guiCheckbox = new GuiCheckbox(guiCheckboxTexture.ToTextureRegion())
            {
                Position = new Vector2(500, 100),
            };
            guiLayer.Controls.Add(guiCheckbox);
        }

        public override void Update(float deltaTime)
        {
            _mouseCursor.Position = InputDevice.Position;
            base.Update(deltaTime);
        }
    }
}