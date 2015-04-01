using Astrid.Framework;
#if ANDROID
using Android.Graphics;
#else
using System.Drawing.Imaging;
#endif
using OpenTK.Graphics.ES20;


namespace Astrid.Windows.Graphics
{
    public class GLTexture : Texture
    {
        public GLTexture(int id, string name, int width, int height, string filePath)
            : base(id, filePath, name, width, height)
        {
        }
    }
}
