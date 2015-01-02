using System.Xml.Serialization;
using Astrid.Core;
using Astrid.Framework.Assets;
using Astrid.Framework.Graphics;

namespace Astrid.Framework.Entities.Components
{
    public class Sprite : Drawable, ITransformable
    {
        public Sprite(TextureRegion textureRegion)
            : this()
        {
            TextureRegion = textureRegion;
        }

        public Sprite()
        {
            Origin = new Vector2(0.5f, 0.5f);
            Color = Color.White;
            Scale = Vector2.One;
            IsVisible = true;
        }

        public TextureRegion TextureRegion { get; set; }
        public Color Color { get; set; }
        public Vector2 Origin { get; set; }
        public Vector2 Position { get; set; }
        public float Rotation { get; set; }
        public Vector2 Scale { get; set; }
        public bool IsVisible { get; set; }

        public override Rectangle GetBoundingRectangle()
        {
            if(Entity == null || TextureRegion == null)
                return Rectangle.Empty;

            var scale = Entity.Scale * Scale;
            var position = Entity.Position + Position;
            var width = TextureRegion.Width * scale.X;
            var height = TextureRegion.Height * scale.Y;
            var x = (int)(position.X - Origin.X * width);
            var y = (int)(position.Y - Origin.Y * height);

            return new Rectangle(x, y, (int)width, (int)height);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (Entity == null || TextureRegion == null || !IsVisible) return;

            var position = Entity.Position + Position;
            var rotation = Entity.Rotation + Rotation;
            var scale = Entity.Scale * Scale;
            spriteBatch.Draw(TextureRegion, position, Color, Origin, rotation, scale);
        }

        public override string ToString()
        {
            return string.Format("{0}", TextureRegion);
        }
    }
}