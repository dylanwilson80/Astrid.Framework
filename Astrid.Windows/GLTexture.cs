#if ANDROID
using Android.Graphics;
#else
#endif


namespace Astrid.Windows
{
    public class GLTexture : Texture
    {
        public GLTexture(int id, string name, int width, int height, string filePath)
            : base(id, filePath, name, width, height)
        {
        }
    }
}
