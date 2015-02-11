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
    public class TextureAtlas : IAsset
    {
        public TextureAtlas(string name, params Texture[] textures)
        {
            Name = name;
            _textures = new List<Texture>(textures);
        }

        public string Name { get; private set; }

        private readonly List<Texture> _textures = new List<Texture>();
        private readonly List<TextureAtlasRegion> _regions = new List<TextureAtlasRegion>();

        /// <summary>
        /// Adds a texture to the atlas.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="textureIndex"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns>The AtlasRegion that was added.</returns>
        public TextureAtlasRegion AddRegion(string name, int textureIndex, int x, int y, int width, int height)
        {
            var texture = _textures[textureIndex];
            var region = new TextureAtlasRegion(name, texture, x, y, width, height)
            {
                OriginalWidth = width,
                OriginalHeight = height,
                Index = -1
            };
            _regions.Add(region);
            return region;
        }

        public TextureAtlasRegion AddRegion(TextureAtlasRegion atlasRegion)
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
        public TextureAtlasRegion FindRegion(string name)
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
        public TextureAtlasRegion FindRegion(string name, int index)
        {
            return _regions.FirstOrDefault(i => i.Name == name && i.Index == index);
        }

        public TextureAtlasRegion this[string name]
        {
            get { return FindRegion(name); }
        }

        public TextureAtlasRegion this[string name, int index]
        {
            get { return FindRegion(name, index); }
        }

        /// <summary>
        /// Returns all regions with the specified name, ordered by smallest to largest index. The results should be cached.
        /// </summary>
        /// <param name="name"></param>
        /// <returns>A possibly empty List of AtlasRegions with a matching name.</returns>
        public List<TextureAtlasRegion> FindRegions(string name)
        {
            return _regions
                .Where(i => i.Name == name)
                .ToList();
        }
    }
}
