using System;
using System.Collections.Generic;
using System.IO;

namespace Astrid
{
    public interface IAsset
    {
        string Name { get; }
    }

    public abstract class AssetManager
    {
        protected AssetManager(IDeviceManager deviceManager)
        {
            DeviceManager = deviceManager;
            _loaders = new Dictionary<Type, IAssetLoader>
            {
                {typeof(Texture), new TextureLoader()},
                {typeof(TextureAtlas), new TextureAtlasLoader()},
                {typeof(SoundEffect), new SoundEffectLoader()}
            };
        }

        private readonly Dictionary<Type, IAssetLoader> _loaders;

        protected IDeviceManager DeviceManager { get; private set; }

        public abstract Stream OpenStream(string path);
        public abstract Texture LoadTexture(string assetPath);
        public abstract SoundEffect LoadSoundEffect(string assetPath);

        public void RegisterLoader<T>(AssetLoader<T> loader)
            where T : IAsset
        {
            var type = typeof (T);

            if (_loaders.ContainsKey(type))
                throw new InvalidOperationException(string.Format("Asset loader already registered for type {0}", type.Name));

            _loaders.Add(type, loader);
        }

        public T Load<T>(string assetPath, AssetLoader<T> loader)
            where T : IAsset
        {
            return loader.Load(this, assetPath);
        }

        public T Load<T>(string assetPath) where T : IAsset
        {
            var type = typeof (T);
            IAssetLoader loader;

            if (_loaders.TryGetValue(type, out loader))
            {
                var loaderT = (AssetLoader<T>) loader;
                return loaderT.Load(this, assetPath);
            }

            throw new InvalidOperationException(string.Format("No asset loader found for type {0}", type.Name));
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
    }
}
