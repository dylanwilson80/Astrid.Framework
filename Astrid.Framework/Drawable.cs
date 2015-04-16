using Astrid.Core;

namespace Astrid
{
    public abstract class Drawable : ITransformable, IColorable
    {
        protected Drawable(TextureRegion textureRegion)
        {
            TextureRegion = textureRegion;
            Origin = new Vector2(0.5f, 0.5f);
            Color = Color.White;
            Scale = Vector2.One;
            IsVisible = true;
        }

        protected Drawable(Texture texture)
            : this(texture.ToTextureRegion())
        {
        }

        public TextureRegion TextureRegion { get; set; }
        public Color Color { get; set; }
        public Vector2 Origin { get; set; }
        public bool IsVisible { get; set; }
        public Vector2 Position { get; set; }
        public float Rotation { get; set; }
        public Vector2 Scale { get; set; }

        public virtual Rectangle GetBoundingRectangle()
        {
            if (TextureRegion == null)
                return Rectangle.Empty;

            var width = TextureRegion.Width * Scale.X;
            var height = TextureRegion.Height * Scale.Y;
            var x = (int)(Position.X - Origin.X * width);
            var y = (int)(Position.Y - Origin.Y * height);
            return new Rectangle(x, y, (int)width, (int)height);
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this);
        }

        public override string ToString()
        {
            return string.Format("{0}", TextureRegion);
        }
    }
}