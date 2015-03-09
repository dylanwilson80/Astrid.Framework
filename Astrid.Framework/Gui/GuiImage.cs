using Astrid.Core;
using Astrid.Framework.Assets;
using Astrid.Framework.Assets.Fonts;
using Astrid.Framework.Graphics;

namespace Astrid.Framework.Gui
{
    public class GuiLabel : GuiControl
    {
        public GuiLabel(BitmapFont font, Sprite sprite) 
            : this(font, sprite, null)
        {
        }

        public GuiLabel(BitmapFont font, Sprite normalSprite, Sprite disabledSprite) 
            : base(normalSprite, disabledSprite)
        {
            _font = font;
            TextColor = Color.White;
        }
        
        private readonly BitmapFont _font;

        public string Text { get; set; }
        public Color TextColor { get; set; }

        protected override void OnTouch(Rectangle shape, Vector2 touchPosition)
        {
            
        }

        protected override void OnRelease(Rectangle shape, Vector2 touchPosition)
        {
            
        }

        protected override Sprite GetSpriteForState()
        {
            return null;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            _font.Draw(spriteBatch, Text, (int) Position.X, (int) Position.Y, TextColor);
        }
    }

    public class GuiImage : GuiControl
    {
        public GuiImage(Texture texture)
            : this(texture.ToSprite())
        {
        }

        public GuiImage(TextureRegion textureRegion)
            : this(textureRegion.ToSprite())
        {
        }

        public GuiImage(Sprite sprite) 
            : base(sprite)
        {
        }

        public GuiImage(Sprite normalSprite, Sprite disabledSprite) 
            : base(normalSprite, disabledSprite)
        {
        }

        protected override void OnTouch(Rectangle shape, Vector2 touchPosition)
        {
        }

        protected override void OnRelease(Rectangle shape, Vector2 touchPosition)
        {
        }

        protected override Sprite GetSpriteForState()
        {
            return null;
        }
    }
}