using Astrid.Core;
using Astrid.Framework.Assets;

namespace Astrid.Framework
{
    public class Sprite
    {
        public Sprite(TextureRegion textureRegion)
        {
            TextureRegion = textureRegion;
            Origin = new Vector2(0.5f, 0.5f);
            Color = Color.White;
            IsVisible = true;
        }

        public Sprite(Texture texture)
            : this(texture.ToTextureRegion())
        {
        }

        public TextureRegion TextureRegion { get; set; }
        public Color Color { get; set; }
        public Vector2 Origin { get; set; }
        public bool IsVisible { get; set; }

        public Rectangle GetBoundingRectangle(Vector2 position, Vector2 scale)
        {
            if(TextureRegion == null)
                return Rectangle.Empty;

            var width = TextureRegion.Width * scale.X;
            var height = TextureRegion.Height * scale.Y;
            var x = (int)(position.X - Origin.X * width);
            var y = (int)(position.Y - Origin.Y * height);

            return new Rectangle(x, y, (int)width, (int)height);
        }

        public override string ToString()
        {
            return string.Format("{0}", TextureRegion);
        }
    }
}