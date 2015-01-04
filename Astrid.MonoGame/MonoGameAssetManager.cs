using System.IO;
using Astrid.Framework.Assets;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Astrid.MonoGame
{
    public class MonoGameAssetManager : AssetManager
    {
        private readonly ContentManager _contentManager;

        public MonoGameAssetManager(ContentManager contentManager)
        {
            _contentManager = contentManager;
        }

        public override Stream OpenStream(string path)
        {
            var contentPath = Path.Combine(_contentManager.RootDirectory, path);
            return TitleContainer.OpenStream(contentPath);
        }

        private static int _nextId = 0;

        public override Astrid.Framework.Assets.Texture LoadTexture(string assetPath)
        {
            var id = _nextId++;
            var texture2d = _contentManager.Load<Texture2D>(assetPath);
            return new MonoGameTexture(texture2d, id, assetPath);
        }

        public override SoundEffect LoadSoundEffect(string assetPath)
        {
            var id = _nextId++;
            var soundEffect = _contentManager.Load<Microsoft.Xna.Framework.Audio.SoundEffect>(assetPath);
            return new MonoGameSoundEffect(soundEffect, id);
        }
    }
}