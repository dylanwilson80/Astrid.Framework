using Astrid.Core;

namespace Astrid.Gui
{
    public class GuiImage : GuiControl
    {
        public GuiImage(Texture texture)
            : this(texture.ToTextureRegion())
        {
        }

        public GuiImage(TextureRegion textureRegion)
        {
            TextureRegion = textureRegion;
        }

        protected override void OnTouch(Rectangle shape, Vector2 touchPosition)
        {
        }

        protected override void OnRelease(Rectangle shape, Vector2 touchPosition)
        {
        }
    }
}