using System.Collections.Generic;
using System.IO;
using Astrid.Framework.Graphics;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Astrid.Framework.Assets
{
    public class TextureAtlasLoader : AssetLoader<TextureAtlas>
    {
        public override TextureAtlas Load(AssetManager assetManager, string assetPath)
        {
            var regionMap = new Dictionary<string, TextureRegion>();

            using (var stream = assetManager.OpenStream(assetPath))
            using (var reader = new StreamReader(stream))
            using (var jsonReader = new JsonTextReader(reader))
            {
                var rss = JObject.Load(jsonReader);
                var image = (string)rss["meta"]["image"];

                var name = image;
                var assetName = image;
                var texture = assetManager.Load<Texture>(assetName);
                var textureAtlas = new TextureAtlas(name, texture);

                foreach (var frame in rss["frames"])
                {
                    var regionName = frame["filename"].ToString();
                    var x = (int)frame["frame"]["x"];
                    var y = (int)frame["frame"]["y"];
                    var w = (int)frame["frame"]["w"];
                    var h = (int)frame["frame"]["h"];
                    var rotated = (bool)frame["rotated"];

                    if (rotated)
                    {
                        var temp = w;
                        w = h;
                        h = temp;
                    }

                    var textureRegion = textureAtlas.AddRegion(regionName, 0, x, y, w, h);
                    regionMap.Add(regionName, textureRegion);
                }

                return textureAtlas;
            }
        }
    }
}
