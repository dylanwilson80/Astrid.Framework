using System.Collections.Generic;
using Astrid.Framework.Assets;

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
    }
}