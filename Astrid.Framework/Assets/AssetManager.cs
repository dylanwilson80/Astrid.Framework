using System;
using System.Collections.Generic;
using System.IO;
using Astrid.Framework.Assets.LibGDX;
using Astrid.Framework.Audio;
using Astrid.Framework.Graphics;
using Astrid.Framework.Scenes;
using Astrid.Framework.Serializers;

namespace Astrid.Framework.Assets
{
    public interface IAsset
    {
        string Name { get; }
    }

    public abstract class AssetManager
    {
        protected AssetManager()
        {
            _loadFunctions = new Dictionary<Type, Func<string, IAsset>>
            {
                {typeof(Texture), LoadTexture},
                {typeof(TextureRegion), LoadTextureRegion},
                {typeof(TextureAtlas), LoadTextureAtlas},
                {typeof(SoundEffect), LoadSoundEffect}
            };
        }

        private readonly Dictionary<Type, Func<string, IAsset>> _loadFunctions;
  
        public abstract Stream OpenStream(string path);
        public abstract Texture LoadTexture(string assetPath);
        public abstract SoundEffect LoadSoundEffect(string assetPath);

        public T Load<T>(string assetPath, AssetLoader<T> loader)
            where T : IAsset
        {
            return loader.Load(this, assetPath);
        }

        public T Load<T>(string assetPath) where T : IAsset
        {
            var type = typeof (T);
            Func<string, IAsset> loadFunction;

            if (_loadFunctions.TryGetValue(type, out loadFunction))
                return (T) loadFunction(assetPath);

            throw new InvalidOperationException(string.Format("No load function found for type {0}", type.Name));
        }

        public TextureAtlas LoadTextureAtlas(string assetPath)
        {
            var loader = new TextureAtlasLoader();
            return loader.Load(this, assetPath);
        }

        private readonly Dictionary<string, TextureRegion> _textureRegions = new Dictionary<string, TextureRegion>();

        public TextureRegion LoadTextureRegion(string name)
        {
            TextureRegion textureRegion;

            if (_textureRegions.TryGetValue(name, out textureRegion))
                return textureRegion;

            var texture = LoadTexture(name);
            textureRegion = new TextureRegion(texture);
            _textureRegions.Add(name, textureRegion);
            return textureRegion;
        }

        //public OrthogonalTileMap LoadOrthogonalTileMap(string assetPath)
        //{
        //    return OrthogonalTileMap.Load(this, assetPath);
        //}

        public Scene LoadScene(string assetPath)
        {
            var parser = new JsonParser(this);

            using (var stream = OpenStream(assetPath))
            using (var reader = new StreamReader(stream))
            {
                return parser.LoadScene(reader);
            }
        }
    }
}
