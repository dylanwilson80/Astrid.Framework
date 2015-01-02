using System.Collections.Generic;
using System.IO;
using System.Linq;
using Astrid.Framework.Assets;
using Astrid.Windows.Audio;
using Astrid.Windows.Graphics;

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

            texture = GLTexture.Load(assetPath, filePath);
            _textureCache.Add(filePath, texture);
            return texture;
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
