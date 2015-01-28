using System.Drawing;
using System.IO;
using Astrid.Framework.Assets;
#if ANDROID
using Android.Graphics;
using OpenTK.Graphics.ES20;
#else
using System.Drawing.Imaging;
using OpenTK.Graphics.OpenGL;
#endif


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
