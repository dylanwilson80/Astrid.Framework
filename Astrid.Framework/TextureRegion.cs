using Astrid.Core;

namespace Astrid.Framework
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

        public string Name { get; protected set; }
        public Texture Texture { get; protected set; }
        public int X { get; private set; }
        public int Y { get; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }
        public Vector2 Centre { get; protected set; }

        private float[] _uv;
        // ReSharper disable once InconsistentNaming
        public float[] GetUV()
        {
            if (_uv == null)
            {
                var u0 = (float) X / Texture.Width;
                var v0 = (float) Y / Texture.Height;
                var u1 = (float) (X + Width) / Texture.Width;
                var v1 = (float) (Y + Height) / Texture.Height;

                // For a 1x1 region, adjust UVs toward pixel center to avoid filtering artifacts on AMD GPUs when 
                // drawing very stretched.
                if (Width == 1 && Height == 1)
                {
                    var adjustX = 0.25f / Texture.Width;
                    u0 += adjustX;
                    u1 -= adjustX;
                    var adjustY = 0.25f / Texture.Height;
                    v0 += adjustY;
                    v1 -= adjustY;
                }

                _uv = new[] {u0, v0, u1, v1};
            }

            return _uv;
        }

        public void Flip(bool x, bool y)
        {
            var uvs = GetUV();
            
            if (x && y) 
                _uv = new[] { uvs[2], uvs[3], uvs[0], uvs[1] };
            else if (x) 
                _uv = new[] { uvs[2], uvs[1], uvs[0], uvs[3] };
            else if (y) 
                _uv = new[] { uvs[0], uvs[3], uvs[2], uvs[1] };
            else 
                _uv = uvs;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
