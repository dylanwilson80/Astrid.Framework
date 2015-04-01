using System.IO;
using Android.Content;
using Android.Graphics;
using Astrid.Framework;
using Astrid.Windows.Graphics;
using OpenTK.Graphics.ES20;

namespace Astrid.Android
{
    public class AndroidAssetManager : AssetManager
    {
        private readonly Context _context;

        public AndroidAssetManager(Context context)
        {
            _context = context;
        }

        public override Stream OpenStream(string path)
        {
            return _context.Assets.Open(path);
        }

        public override Texture LoadTexture(string assetPath)
        {
            int id;
            GL.GenTextures(1, out id);
            GL.BindTexture(TextureTarget.Texture2D, id);

            using (var stream = OpenStream(assetPath))
            using (var bitmap = BitmapFactory.DecodeStream(stream))
            {
                var pixels = bitmap.LockPixels();

                GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, bitmap.Width, bitmap.Height, 0,
                   OpenTK.Graphics.ES20.PixelFormat.Rgba, PixelType.UnsignedByte, pixels);

                bitmap.UnlockPixels();

                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.ClampToEdge);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.ClampToEdge);
                
                //GLUtils.TexImage2D((int)TextureTarget.Texture2D, 0, bitmap, 0);
                GL.Flush();

                var texture = new GLTexture(id, assetPath, bitmap.Width, bitmap.Height, assetPath);
                bitmap.Recycle();
                return texture;
            }
        }

        public override SoundEffect LoadSoundEffect(string assetPath)
        {
            throw new System.NotImplementedException();
        }
    }
}