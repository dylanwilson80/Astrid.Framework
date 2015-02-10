using Astrid.Core;

namespace Astrid.Framework.Assets
{
    public class TextureRegion : IAsset
    {
        public TextureRegion()
        {

        }
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
        public int X { get; protected set; }
        public int Y { get; protected set; }
        public int Width { get; protected set; }
        public int Height { get; protected set; }
        public Vector2 Centre { get; protected set; }

        protected float[] _uv;
        public float[] UV
        {
            get
            {
                return GetUV();
            }
            protected set { _uv = value; }
        }
        public float[] GetUV()
        {
            if (_uv == null)
            {
                float u0 = (float) X/Texture.Width;
                float v0 = (float) Y/Texture.Height;
                float u1 = (float) (X + Width)/Texture.Width;
                float v1 = (float) (Y + Height)/Texture.Height;

                // For a 1x1 region, adjust UVs toward pixel center to avoid filtering artifacts on AMD GPUs when drawing very stretched.
                if (Width == 1 && Height == 1)
                {
                    float adjustX = 0.25f / Texture.Width;
                    u0 += adjustX;
                    u1 -= adjustX;
                    float adjustY = 0.25f / Texture.Height;
                    v0 += adjustY;
                    v1 -= adjustY;
                }
                _uv = new float[] {u0, v0, u1, v1};
            }

            return _uv;
        }
        public void Flip(bool x, bool y)
        {
            float[] uvs = GetUV();
            if (x && y) _uv = new float[] { uvs[2], uvs[3], uvs[0], uvs[1] };
            else if (x) _uv = new float[] { uvs[2], uvs[1], uvs[0], uvs[3] };
            else if (y) _uv = new float[] { uvs[0], uvs[3], uvs[2], uvs[1] };
            else _uv = uvs;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}