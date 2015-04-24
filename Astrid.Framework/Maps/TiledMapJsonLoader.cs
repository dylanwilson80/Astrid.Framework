using System.IO;
using Newtonsoft.Json;

namespace Astrid.Maps
{
    public class TiledMapJsonLoader : AssetLoader<TiledMap>
    {
        public override TiledMap Load(AssetManager assetManager, string assetPath)
        {
            using (var stream = assetManager.OpenStream(assetPath))
            using (var streamReader = new StreamReader(stream))
            using (var jsonReader = new JsonTextReader(streamReader))
            {
                var tiledMap = JsonSerializer.Create().Deserialize<TiledMap>(jsonReader);
                tiledMap.Name = assetPath;
                return tiledMap;
            }
        }
    }
}
