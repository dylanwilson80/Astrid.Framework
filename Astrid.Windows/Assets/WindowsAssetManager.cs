using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using Astrid.Framework.Assets;
using Astrid.Windows.Audio;
using Astrid.Windows.Graphics;
using OpenTK.Graphics.OpenGL;

namespace Astrid.Windows.Assets
{
    public class WindowsAssetManager : AssetManager
    {
        public WindowsAssetManager(string contentPath)
        {
            _contentPath = contentPath;
        }

        private readonly string _contentPath;

        public override Stream OpenStream(string path)
        {
            var filePath = Path.Combine(_contentPath, path);
            return new FileStream(filePath, FileMode.Open, FileAccess.Read);
        }

        private readonly Dictionary<string, GLTexture> _textureCache = new Dictionary<string, GLTexture>(); 

        public override Texture LoadTexture(string assetPath)
        {
            var filePath = Path.Combine(_contentPath, assetPath);

            GLTexture texture;

            if (_textureCache.TryGetValue(filePath, out texture))
                return texture;

            if (!File.Exists(filePath))
                throw new FileNotFoundException(string.Format("File not found loading GL Texture: {0}", filePath), filePath);
            
            texture = LoadGLTexture(assetPath, filePath);
            _textureCache.Add(filePath, texture);
            return texture;
        }

        private GLTexture LoadGLTexture(string name, string filePath)
        {
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
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.ClampToEdge);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.ClampToEdge);

                return new GLTexture(id, name, bitmap.Width, bitmap.Height, filePath);
            }
        }

        public override SoundEffect LoadSoundEffect(string assetPath)
        {
            var filePath = Path.Combine(_contentPath, assetPath);
            return NAudioSoundEffect.Load(filePath);
        }

        public string[] GetFiles(string searchPattern)
        {
            return Directory.GetFiles(_contentPath, searchPattern)
                .Select(Path.GetFileName)
                .ToArray();
        }
    }
}
