using Astrid.Core;

namespace Astrid
{
    public class Sprite : Drawable
    {
        public Sprite(TextureRegion textureRegion) 
            : base(textureRegion)
        {
        }

        public Sprite(Texture texture) 
            : base(texture)
        {
        }

        public static Sprite Create(Texture texture, int x, int y, int width, int height)
        {
            return Create(texture.ToTextureRegion(), x, y, width, height);
        }

        public static Sprite Create(TextureRegion textureRegion, int x, int y, int width, int height)
        {
            var regionWidth = (float) textureRegion.Width;
            var regionHeight = (float) textureRegion.Height;
            var scaleX = regionWidth / width;
            var scaleY = regionHeight/height;
            return new Sprite(textureRegion)
            {
                Origin = Vector2.Zero,
                Position = new Vector2(x, y),
                Scale = new Vector2(scaleX, scaleY)
            };
        }
    }
}