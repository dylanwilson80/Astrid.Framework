using System.Collections.Generic;

namespace Astrid
{
    public class TextureAtlas : IAsset
    {
        public TextureAtlas(string name, Texture texture)
        {
            Name = name;
            Texture = texture;
        }

        public string Name { get; private set; }
        public Texture Texture { get; private set; }

        private readonly Dictionary<string, TextureRegion> _regions = new Dictionary<string, TextureRegion>();
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

        public TextureRegion AddRegion(TextureRegion region)
        {
            _regions.Add(region.Name, region);
            return region;
        }

        public void RemoveRegion(string name)
        {
            if(_regions.ContainsKey(name))
                _regions.Remove(name);
        }

        public void RemoveRegion(TextureRegion region)
        {
            if(_regions.ContainsKey(region.Name))
                _regions.Remove(region.Name);
        }

        public TextureRegion FindRegion(string name)
        {
            return _regions[name];
        }

        public TextureRegion this[string name]
        {
            get { return FindRegion(name); }
        }
    }
}
