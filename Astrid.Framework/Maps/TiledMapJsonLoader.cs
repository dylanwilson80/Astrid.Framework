using System.IO;
using Newtonsoft.Json;

namespace Astrid.Maps
{
    public class TiledMapJsonLoader : AssetLoader<TiledMap>
    {
        private readonly GraphicsDevice _graphicsDevice;

        public TiledMapJsonLoader(GraphicsDevice graphicsDevice)
        {
            _graphicsDevice = graphicsDevice;
        }

        public override TiledMap Load(AssetManager assetManager, string assetPath)
        {
            using (var stream = assetManager.OpenStream(assetPath))
            using (var streamReader = new StreamReader(stream))
            using (var jsonReader = new JsonTextReader(streamReader))
            {
                var data = JsonSerializer.Create().Deserialize<TiledMapData>(jsonReader);
                return new TiledMap(assetManager, _graphicsDevice, assetPath, data);
            }
        }
    }
}
