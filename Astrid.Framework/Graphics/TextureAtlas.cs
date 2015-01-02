using System.Collections.Generic;
using System.IO;
using Astrid.Framework.Assets;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Astrid.Framework.Graphics
{
    public class TextureAtlas : IAsset
    {
        public TextureAtlas(string name, Texture texture)
        {
            Name = name;
            Texture = texture;
            _regions = new Dictionary<string, TextureRegion>();
        }

        public string Name { get; private set; }
        public Texture Texture { get; private set; }

        private readonly Dictionary<string, TextureRegion> _regions;
        public IEnumerable<TextureRegion> Regions
        {
            get { return _regions.Values; }
        }

        public TextureRegion AddRegion(string name, int x, int y, int width, int height)
        {
            var region = new TextureRegion(name, Texture, x, y, width, height);
            _regions.Add(name, region);
            return region;
        }

        public void RemoveRegion(string name)
        {
            _regions.Remove(name);
        }

        public TextureRegion GetRegion(string name)
        {
            TextureRegion textureRegion;

            if (_regions.TryGetValue(name, out textureRegion))
                return textureRegion;

            return null;
        }

        public static TextureAtlas Load(AssetManager assetManager, string assetName)
        {
            using (var stream = assetManager.OpenStream(assetName))
            {
                return Load(assetManager, stream);
            }
        }

        public static TextureAtlas Load(AssetManager assetManager, Stream stream)
        {
            var regionMap = new Dictionary<string, TextureRegion>();

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

                    var textureRegion = textureAtlas.AddRegion(regionName, x, y, w, h);
                    regionMap.Add(regionName, textureRegion);
                }

                return textureAtlas;
            }
        }
    }
}