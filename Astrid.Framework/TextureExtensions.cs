namespace Astrid
{
    public static class TextureExtensions
    {
        public static TextureRegion ToTextureRegion(this Texture texture)
        {
            return new TextureRegion(texture);
        }

        public static Sprite ToSprite(this TextureRegion textureRegion)
        {
            return new Sprite(textureRegion);
        }

        public static Sprite ToSprite(this Texture texture)
        {
            return texture.ToTextureRegion().ToSprite();
        }
    }
}