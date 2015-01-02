using Astrid.Core;

namespace Astrid.Framework.Assets
{
    public class TextureRegion : IAsset
    {
        public TextureRegion(Texture texture)
            : this(texture.Name, texture, 0, 0, texture.Width, texture.Height)
        {
        }

        public TextureRegion(string name, Texture texture, int x, int y, int width, int height)
        {
            Name = name;
            Texture = texture;
            X = x;
            Y = y;
            Width = width;
            Height = height;
            Centre = new Vector2(Width*0.5f, Height*0.5f);
        }

        public string Name { get; private set; }
        public Texture Texture { get; private set; }
        public int X { get; private set; }
        public int Y { get; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }
        public Vector2 Centre { get; private set; }

        private float[] _uv;
        public float[] GetUV()
        {
            if (_uv == null)
            {
                var u0 = (float) X/Texture.Width;
                var v0 = (float) Y/Texture.Height;
                var u1 = (float) (X + Width)/Texture.Width;
                var v1 = (float) (Y + Height)/Texture.Height;

                _uv = new[] {u0, v0, u1, v1};
            }

            return _uv;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}