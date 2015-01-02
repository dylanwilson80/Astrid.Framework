using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using Astrid.Framework.Assets;
using OpenTK.Graphics.OpenGL;

namespace Astrid.Windows.Graphics
{
    public class GLTexture : Texture
    {
        public GLTexture(int id, string name, int width, int height, string filePath)
            : base(id, filePath, name, width, height)
        {
        }

        public static GLTexture Load(string name, string filePath)
        {
            if (!File.Exists(filePath)) 
                throw new FileNotFoundException(string.Format("File not found loading GL Texture: {0}", filePath), filePath);

            int id;
            GL.GenTextures(1, out id);
            GL.BindTexture(TextureTarget.Texture2D, id);

            using (var bitmap = new Bitmap(filePath))
            {
                const System.Drawing.Imaging.PixelFormat pixelFormat = System.Drawing.Imaging.PixelFormat.Format32bppArgb;
                var rectangle = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
                var data = bitmap.LockBits(rectangle, ImageLockMode.ReadOnly, pixelFormat);

                GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, data.Width, data.Height, 0,
                   OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);
                
                bitmap.UnlockBits(data);
                
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.ClampToBorder);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.ClampToBorder);

                return new GLTexture(id, name, bitmap.Width, bitmap.Height, filePath);
            }
        }
    }
}
