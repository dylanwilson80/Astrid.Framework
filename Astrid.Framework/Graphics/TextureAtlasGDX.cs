using System;
using System.Collections.Generic;
using System.IO;
using Astrid.Framework.Assets;
using System.Linq;
using System.Text;

namespace Astrid.Framework.Graphics
{
    /// <summary>
    /// Provides TextureAtlasGDX with the ability to read texture atlas files in
    /// libGDX's format. This is not meant for everyday use.
    /// </summary>
    /// <see cref="TextureAtlasGDX" />
    public class TextureAtlasData
    {
        static internal String[] _tuple = new String[4];

        static internal string readValue(StreamReader reader)
        {
            String line = reader.ReadLine();
            int colon = line.IndexOf(':');
            if (colon == -1) throw new FormatException("Invalid line: " + line);
            return line.Substring(colon + 1).Trim();
        }

        /// <summary>
        /// Reads a a group of strings separated by commas after a colon.
        /// What precedes the colon is ignored.
        /// </summary>
        /// <param name="reader"></param>
        /// <returns>The number of tuple values read (1, 2 or 4).</returns>
        static internal int readTuple(StreamReader reader)
        {
            String line = reader.ReadLine();
            int colon = line.IndexOf(':');
            if (colon == -1) throw new FormatException("Invalid line: " + line);
            int i = 0, lastMatch = colon + 1;
            for (i = 0; i < 3; i++)
            {
                int comma = line.IndexOf(',', lastMatch);
                if (comma < 0) break;
                _tuple[i] = line.Substring(lastMatch, comma - lastMatch).Trim();
                lastMatch = comma + 1;
            }
            _tuple[i] = line.Substring(lastMatch).Trim();
            return i + 1;
        }
        /// <summary>
        /// A texture page that may contain multiple Regions. This is not meant for everyday use.
        /// </summary>
        /// <see cref="TextureAtlasGDX" />
        public class Page
        {
            public readonly string TextureHandle;
            public Texture Texture;
            public readonly float Width, Height;
            public readonly bool UseMipMaps;
            /*
            public readonly Format format;
            public readonly TextureFilter minFilter;
            public readonly TextureFilter magFilter;
            public readonly TextureWrap uWrap;
            public readonly TextureWrap vWrap;*/

            public Page(string textureHandle, float width, float height, bool useMipMaps
                /*, Format format, TextureFilter minFilter, TextureFilter magFilter, TextureWrap uWrap, TextureWrap vWrap */)
            {
                this.Width = width;
                this.Height = height;
                this.TextureHandle = textureHandle;
                this.UseMipMaps = useMipMaps;
                /*
                this.format = format;
                this.minFilter = minFilter;
                this.magFilter = magFilter;
                this.uWrap = uWrap;
                this.vWrap = vWrap;
                 */
            }
        }
        /// <summary>
        /// An internally-used class that keeps information about a portion of a Page.
        /// This is not meant for everyday use.
        /// </summary>
        /// <see cref="TextureAtlasGDX" />
        public class Region : IComparable<Region>
        {
            public Page Page;
            public int Index;
            public String Name;
            public float OffsetX;
            public float OffsetY;
            public int OriginalWidth;
            public int OriginalHeight;
            public bool Rotate;
            public int Left;
            public int Top;
            public int Width;
            public int Height;
            public bool Flip;
            public int[] Splits;
            public int[] Pads;

            public int CompareTo(Region other)
            {
                int i1 = Index;
                if (i1 == -1) i1 = int.MaxValue;
                int i2 = other.Index;
                if (i2 == -1) i2 = int.MaxValue;
                return i1 - i2;
            }

            public override string ToString()
            {
                return "Region " + this.Name + " with index " + this.Index;
            }
        }

        internal readonly List<Page> pages = new List<Page>();
        internal readonly List<Region> regions = new List<Region>();

        public List<Page> Pages { get { return pages; } }
        public List<Region> Regions { get { return regions; } }

        public TextureAtlasData(AssetManager assetManager, Stream packFile, string imageFolder, bool flip)
        {
            using (StreamReader reader = new StreamReader(packFile))
            {
                try
                {
                    Page pageImage = null;
                    while (true)
                    {
                        String line = reader.ReadLine();
                        if (line == null) break;
                        if (line.Trim().Length == 0)
                            pageImage = null;
                        else if (pageImage == null)
                        {
                            if (imageFolder != null && imageFolder != "" && imageFolder.EndsWith("/") == false)
                                imageFolder += "/";
                            string textureHandle = imageFolder + line;

                            float width = 0, height = 0;
                            if (readTuple(reader) == 2)
                            { // size is only optional for an atlas packed with an old TexturePacker.
                                width = int.Parse(_tuple[0]);
                                height = int.Parse(_tuple[1]);
                                readTuple(reader);
                            }
                            //Format format = Format.valueOf(tuple[0]);

                            readTuple(reader);
                            //TextureFilter min = TextureFilter.valueOf(tuple[0]);
                            //TextureFilter max = TextureFilter.valueOf(tuple[1]);

                            String direction = readValue(reader);
                            //TextureWrap repeatX = ClampToEdge;
                            //TextureWrap repeatY = ClampToEdge;
                            /*
                            switch(direction)
                            {
                                case "xy": repeatX = Repeat; repeatY = Repeat;
                                    break;
                                case "x": repeatX = Repeat;
                                    break;
                                case "y": repeatY = Repeat;
                                    break;
                            }*/
                            pageImage = new Page(textureHandle, width, height, false/* min.isMipMap(), format, min, max, repeatX, repeatY*/);
                            pages.Add(pageImage);
                        }
                        else
                        {
                            bool rotate = bool.Parse(readValue(reader));

                            readTuple(reader);
                            int left = int.Parse(_tuple[0]);
                            int top = int.Parse(_tuple[1]);

                            readTuple(reader);
                            int width = int.Parse(_tuple[0]);
                            int height = int.Parse(_tuple[1]);

                            Region region = new Region();
                            region.Page = pageImage;
                            region.Left = left;
                            region.Top = top;
                            region.Width = width;
                            region.Height = height;
                            region.Name = line;
                            region.Rotate = rotate;

                            if (readTuple(reader) == 4)
                            { // split is optional
                                region.Splits = new int[] {int.Parse(_tuple[0]), int.Parse(_tuple[1]),
								int.Parse(_tuple[2]), int.Parse(_tuple[3])};

                                if (readTuple(reader) == 4)
                                { // pad is optional, but only present with splits
                                    region.Pads = new int[] {int.Parse(_tuple[0]), int.Parse(_tuple[1]),
									int.Parse(_tuple[2]), int.Parse(_tuple[3])};

                                    readTuple(reader);
                                }
                            }

                            region.OriginalWidth = int.Parse(_tuple[0]);
                            region.OriginalHeight = int.Parse(_tuple[1]);

                            readTuple(reader);
                            region.OffsetX = int.Parse(_tuple[0]);
                            region.OffsetY = int.Parse(_tuple[1]);

                            region.Index = int.Parse(readValue(reader));

                            if (flip) region.Flip = true;

                            regions.Add(region);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new FormatException("Error reading pack file: " + packFile, ex);
                }
            }
            regions.Sort();

        }

    }

    /// <summary>
    /// A substitute for the TextureAtlas class that can read in atlases created for use by libGDX.
    /// </summary>
    /// <remarks>
    /// These atlases can be created by libGDX itself, by commercial texture packers, or by https://github.com/tommyettinger/GDXTexturePacker .
    /// </remarks>
    public class TextureAtlasGDX : IAsset
    {
        public string Name { get; private set; }
        internal AssetManager assetManager;
        internal readonly HashSet<Texture> textures = new HashSet<Texture>();
        internal readonly List<AtlasRegion> regions = new List<AtlasRegion>();
        /// <summary>
        /// All the <c>Texture</c>s used by this atlas.
        /// </summary>
        public HashSet<Texture> Textures { get { return textures; } }
        /// <summary>
        /// All the <c>AtlasRegion</c>s stored in this atlas. 
        /// </summary>
        /// <remarks>
        /// Looking up by name with [] is preferred to iterating through this List.
        /// You can also look up with name and index into an animation.
        /// </remarks>
        public List<AtlasRegion> Regions { get { return regions; } }

        /// <summary>
        /// The preferred constructor. All pages of the atlas must be load-able as <c>Texture</c> Assets.
        /// </summary>
        /// <param name="assetManager">Used to load the images that make up the atlas.</param>
        /// <param name="packFile">Should be a Stream that reads from an atlas file.</param>
        /// <param name="imageFolder">Optional, defaults to empty string. If the page <c>Texture</c>s are in a subfolder
        /// of the content folder, give the sub-path that leads to the page images here.</param>
        /// <param name="flip">Optional, defaults to false. Flip the textures vertically.</param>
        public TextureAtlasGDX(AssetManager assetManager, Stream packFile, string imageFolder = "", bool flip = false)
            : this(assetManager, new TextureAtlasData(assetManager, packFile, imageFolder, flip))
        {
        }
        /// <summary>
        /// Not recommended for external use.
        /// </summary>
        /// <param name="assetManager"></param>
        /// <param name="data"></param>
        public TextureAtlasGDX(AssetManager assetManager, TextureAtlasData data)
        {
            this.assetManager = assetManager;
            if (data != null) load(data);
        }

        public static TextureAtlasGDX Load(AssetManager assetManager, string assetName)
        {
            using (var stream = assetManager.OpenStream(assetName))
            {
                return new TextureAtlasGDX(assetManager, stream);
            }
        }

        private void load(TextureAtlasData data)
        {
            Dictionary<TextureAtlasData.Page, Texture> pageToTexture = new Dictionary<TextureAtlasData.Page, Texture>();
            foreach (TextureAtlasData.Page page in data.pages)
            {
                Texture texture = null;
                if (page.Texture == null)
                {
                    texture = assetManager.Load<Texture>(page.TextureHandle);
                    /*texture.setFilter(page.minFilter, page.magFilter);
                    texture.setWrap(page.uWrap, page.vWrap);*/
                }
                else
                {
                    texture = page.Texture;
                    /*texture.setFilter(page.minFilter, page.magFilter);
                    texture.setWrap(page.uWrap, page.vWrap);*/
                }
                textures.Add(texture);
                pageToTexture.Add(page, texture);
            }

            foreach (TextureAtlasData.Region region in data.regions)
            {
                int width = region.Width;
                int height = region.Height;
                AtlasRegion atlasRegion = new AtlasRegion(region.Name, pageToTexture[region.Page], region.Left, region.Top,
                    region.Rotate ? height : width, region.Rotate ? width : height);
                atlasRegion.Index = region.Index;
                atlasRegion.OffsetX = region.OffsetX;
                atlasRegion.OffsetY = region.OffsetY;
                atlasRegion.OriginalHeight = region.OriginalHeight;
                atlasRegion.OriginalWidth = region.OriginalWidth;
                atlasRegion.Rotate = region.Rotate;
                atlasRegion.Splits = region.Splits;
                atlasRegion.Pads = region.Pads;
                if (region.Flip) atlasRegion.Flip(false, true);
                regions.Add(atlasRegion);
            }
        }

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
        public AtlasRegion addRegion(String name, Texture texture, int x, int y, int width, int height)
        {
            textures.Add(texture);
            AtlasRegion region = new AtlasRegion(name, texture, x, y, width, height);
            region.OriginalWidth = width;
            region.OriginalHeight = height;
            region.Index = -1;
            regions.Add(region);
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
        /// <returns>The AtlasRegion that was added.</returns>
        public AtlasRegion addRegion(String name, TextureRegion textureRegion)
        {
            return addRegion(name, textureRegion.Texture, textureRegion.X, textureRegion.Y,
                textureRegion.Width, textureRegion.Height);
        }

        /// <summary>
        /// Returns the first region found with the specified name. The result should be cached.
        /// Square-bracket access with one string parameter is an alias for this method.
        /// </summary>
        /// <param name="name"></param>
        /// <returns>The AtlasRegion if found, or null.</returns>
        public AtlasRegion findRegion(string name)
        {
            for (int i = 0, n = regions.Count; i < n; i++)
                if (regions[i].Name == name) return regions[i];
            return null;
        }

        /// <summary>
        /// Returns the first region found with the specified name and the specified index. The result should be cached.
        /// Square-bracket access with one string and one int parameter is an alias for this method.
        /// </summary>
        /// <param name="name"></param>
        /// <returns>The AtlasRegion if found, or null.</returns>
        public AtlasRegion findRegion(string name, int index)
        {
            for (int i = 0, n = regions.Count; i < n; i++)
            {
                AtlasRegion region = regions[i];
                if (region.Name != name) continue;
                if (region.Index != index) continue;
                return region;
            }
            return null;
        }
        public AtlasRegion this[string name]
        {
            get { return findRegion(name); }
        }
        public AtlasRegion this[string name, int idx]
        {
            get { return findRegion(name, idx); }
        }

        /// <summary>
        /// Returns all regions with the specified name, ordered by smallest to largest index. The results should be cached.
        /// </summary>
        /// <param name="name"></param>
        /// <returns>A possibly empty List of AtlasRegions with a matching name.</returns>
        public List<AtlasRegion> findRegions(string name)
        {
            List<AtlasRegion> matched = new List<AtlasRegion>();
            for (int i = 0, n = regions.Count; i < n; i++)
            {
                AtlasRegion region = regions[i];
                if (region.Name == name) matched.Add(new AtlasRegion(region));
            }
            return matched;
        }

    }
    public class AtlasRegion : TextureRegion
    {
        /*
         * When sprites are packed, if the original file name ends with a number, it is stored as the index and is not considered as
         * part of the sprite's name. This is useful for keeping animation frames in order.
         * @see TextureAtlas#findRegions(String) */
        /// <summary>
        /// The number at the end of the original image file name, or -1 if none.
        /// </summary>
        /// <remarks>
        /// When sprites are packed, if the original file name ends with a number, it is stored as the index and is not considered as
        /// part of the sprite's name. This is useful for keeping animation frames in order.
        /// </remarks>
        public int Index { get; set; }

        /// <summary>
        /// The offset from the left of the original image to the left of the packed image, after whitespace was removed for packing.
        /// </summary>
        public float OffsetX;

        /// <summary>
        /// The offset from the bottom of the original image to the bottom of the packed image, after whitespace was removed for packing.
        /// </summary>
        public float OffsetY;

        /// <summary>
        /// The width of the image, after whitespace was removed for packing.
        /// </summary>
        public int PackedWidth;

        /// <summary>
        /// The height of the image, after whitespace was removed for packing.
        /// </summary>
        public int PackedHeight;

        /// <summary>
        /// The width of the image, before whitespace was removed and rotation was applied for packing.
        /// </summary>
        public int OriginalWidth;

        /// <summary>
        /// The height of the image, before whitespace was removed and rotation was applied for packing.
        /// </summary>
        public int OriginalHeight;

        /// <summary>
        /// If true, the region has been rotated 90 degrees counter clockwise.
        /// </summary>
        public bool Rotate;

        /// <summary>
        /// The ninepatch splits, or null if not a ninepatch. Has 4 elements: left, right, top, bottom. Currently unused.
        /// </summary>
        public int[] Splits;

        /// <summary>
        /// The ninepatch pads, or null if not a ninepatch or the has no padding. Has 4 elements: left, right, top, bottom. Currently unused.
        /// </summary>
        public int[] Pads;

        /// <summary>
        /// Empty constructor; you will need to set the fields by hand.
        /// </summary>
        public AtlasRegion()
        {

        }

        /// <summary>
        /// A preferred constructor for AtlasRegions. Will assign -1 as an index (meaning it's not part
        /// of an animation), so it shouldn't share a name with another region.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="texture"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public AtlasRegion(string name, Texture texture, int x, int y, int width, int height)
            : base(name, texture, x, y, width, height)
        {
            Index = -1;
            OriginalWidth = width;
            OriginalHeight = height;
            PackedWidth = width;
            PackedHeight = height;
        }
        /// <summary>
        /// A preferred constructor for AtlasRegions if you already have an AtlasRegion.
        /// </summary>
        /// <param name="region">An AtlasRegion to copy. This cannot be a TextureRegion, it must be an AtlasRegion.</param>
        public AtlasRegion(AtlasRegion region)
            : base(region.Texture)
        {
            Index = region.Index;
            Name = region.Name;
            OffsetX = region.OffsetX;
            OffsetY = region.OffsetY;
            PackedWidth = region.PackedWidth;
            PackedHeight = region.PackedHeight;
            OriginalWidth = region.OriginalWidth;
            OriginalHeight = region.OriginalHeight;
            Rotate = region.Rotate;
            Splits = region.Splits;
        }

        /// <summary>
        /// Flips the region, adjusting the offset so the image appears to be flipped as if no whitespace has been removed for packing.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public new void Flip(bool x, bool y)
        {
            base.Flip(x, y);
            if (x) OffsetX = OriginalWidth - OffsetX - getRotatedPackedWidth();
            if (y) OffsetY = OriginalHeight - OffsetY - getRotatedPackedHeight();
        }

        /// <summary>
        /// Returns the packed width considering the rotate value; if it is true then it returns the packedHeight, otherwise it
        /// returns the packedWidth.
        /// </summary>
        /// <returns></returns>
        public float getRotatedPackedWidth()
        {
            return Rotate ? PackedHeight : PackedWidth;
        }

        /// <summary>
        /// Returns the packed height considering the rotate value, if it is true then it returns the packedWidth, otherwise it
        /// returns the packedHeight.
        /// </summary>
        /// <returns></returns>
        public float getRotatedPackedHeight()
        {
            return Rotate ? PackedWidth : PackedHeight;
        }

    }
}
