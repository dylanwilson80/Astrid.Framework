using System.Collections.Generic;
using Astrid.Framework.Assets;
using System.Linq;

namespace Astrid.Framework.Graphics
{
    /// <summary>
    /// A substitute for the TextureAtlas class that can read in atlases created for use by libGDX.
    /// </summary>
    /// <remarks>
    /// These atlases can be created by libGDX itself, by commercial texture packers, or by https://github.com/tommyettinger/GDXTexturePacker .
    /// </remarks>
    public class TextureAtlasGdx : IAsset
    {
        public TextureAtlasGdx(string name, params Texture[] textures)
        {
            Name = name;
            _textures = new HashSet<Texture>(textures);
        }

        public string Name { get; private set; }

        private readonly HashSet<Texture> _textures = new HashSet<Texture>();
        private readonly List<AtlasRegion> _regions = new List<AtlasRegion>();

        /// <summary>
        /// Adds a texture to the atlas.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="texture"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns>The AtlasRegion that was added.</returns>
        public AtlasRegion AddRegion(string name, Texture texture, int x, int y, int width, int height)
        {
            _textures.Add(texture);
            var region = new AtlasRegion(name, texture, x, y, width, height)
            {
                OriginalWidth = width,
                OriginalHeight = height,
                Index = -1
            };
            _regions.Add(region);
            return region;
        }

        /// <summary>
        /// Adds a TextureRegion to the atlas.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="texture"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="region"></param>
        /// <returns>The AtlasRegion that was added.</returns>
        public AtlasRegion AddRegion(string name, TextureRegion region)
        {
            return AddRegion(name, region.Texture, region.X, region.Y, region.Width, region.Height);
        }

        public AtlasRegion AddRegion(AtlasRegion atlasRegion)
        {
            _regions.Add(atlasRegion);
            return atlasRegion;
        }

        /// <summary>
        /// Returns the first region found with the specified name. The result should be cached.
        /// Square-bracket access with one string parameter is an alias for this method.
        /// </summary>
        /// <param name="name"></param>
        /// <returns>The AtlasRegion if found, or null.</returns>
        public AtlasRegion FindRegion(string name)
        {
            return _regions.FirstOrDefault(i => i.Name == name);
        }

        /// <summary>
        /// Returns the first region found with the specified name and the specified index. The result should be cached.
        /// Square-bracket access with one string and one int parameter is an alias for this method.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="index"></param>
        /// <returns>The AtlasRegion if found, or null.</returns>
        public AtlasRegion FindRegion(string name, int index)
        {
            return _regions.FirstOrDefault(i => i.Name == name && i.Index == index);
        }

        public AtlasRegion this[string name]
        {
            get { return FindRegion(name); }
        }

        public AtlasRegion this[string name, int index]
        {
            get { return FindRegion(name, index); }
        }

        /// <summary>
        /// Returns all regions with the specified name, ordered by smallest to largest index. The results should be cached.
        /// </summary>
        /// <param name="name"></param>
        /// <returns>A possibly empty List of AtlasRegions with a matching name.</returns>
        public List<AtlasRegion> FindRegions(string name)
        {
            return _regions
                .Where(i => i.Name == name)
                .ToList();
        }
    }
}
