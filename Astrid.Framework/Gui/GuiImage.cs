using Astrid.Core;
using Astrid.Framework.Assets;
using Astrid.Framework.Graphics;

namespace Astrid.Framework.Gui
{
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