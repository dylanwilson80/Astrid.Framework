using Astrid.Core;
using Astrid.Framework.Assets;

namespace Astrid.Framework.Graphics
{
    public class Sprite : SceneNode
    {
        public Sprite(TextureRegion textureRegion, Vector2 position)
            : this(position)
        {
            TextureRegion = textureRegion;
        }

        public Sprite(TextureRegion textureRegion)
            : this(textureRegion, Vector2.Zero)
        {
        }

        public Sprite()
            : this(Vector2.Zero)
        {
        }

        public Sprite(Vector2 position)
            : base(position)
        {
            Origin = new Vector2(0.5f, 0.5f);
            Color = Color.White;
            IsVisible = true;
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