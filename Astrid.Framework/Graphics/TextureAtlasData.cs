using System;
using System.Collections.Generic;
using System.IO;
using Astrid.Framework.Assets;

namespace Astrid.Framework.Graphics
{
    /// <summary>
    /// Provides TextureAtlasGDX with the ability to read texture atlas files in
    /// libGDX's format. This is not meant for everyday use.
    /// </summary>
    /// <see cref="TextureAtlasGdx" />
    public class TextureAtlasData
    {
        private static readonly string[] _tuple = new string[4];

        private static string ReadValue(StreamReader reader)
        {
            var line = reader.ReadLine();
            var colon = line.IndexOf(':');
            if (colon == -1) 
                throw new FormatException("Invalid line: " + line);
            return line.Substring(colon + 1).Trim();
        }

        /// <summary>
        /// Reads a a group of strings separated by commas after a colon.
        /// What precedes the colon is ignored.
        /// </summary>
        /// <param name="reader"></param>
        /// <returns>The number of tuple values read (1, 2 or 4).</returns>
        private static int ReadTuple(StreamReader reader)
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
        /// <see cref="TextureAtlasGdx" />
        public class Page
        {
            public readonly string TextureHandle;
            public Texture Texture;
            public readonly float Width, Height;
            public readonly bool UseMipMaps;
            
            //public readonly Format format;
            //public readonly TextureFilter minFilter;
            //public readonly TextureFilter magFilter;
            //public readonly TextureWrap uWrap;
            //public readonly TextureWrap vWrap;

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
        /// <see cref="TextureAtlasGdx" />
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

        private readonly List<Page> _pages = new List<Page>();
        public List<Page> Pages
        {
            get { return _pages; }
        }

        private readonly List<Region> _regions = new List<Region>();
        public List<Region> Regions
        {
            get { return _regions; }
        }

        public TextureAtlasData(Stream stream, string imageFolder, bool flip)
        {
            using (var reader = new StreamReader(stream))
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
                            if (ReadTuple(reader) == 2)
                            { // size is only optional for an atlas packed with an old TexturePacker.
                                width = int.Parse(_tuple[0]);
                                height = int.Parse(_tuple[1]);
                                ReadTuple(reader);
                            }
                            //Format format = Format.valueOf(tuple[0]);

                            ReadTuple(reader);
                            //TextureFilter min = TextureFilter.valueOf(tuple[0]);
                            //TextureFilter max = TextureFilter.valueOf(tuple[1]);

                            String direction = ReadValue(reader);
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
                            _pages.Add(pageImage);
                        }
                        else
                        {
                            bool rotate = bool.Parse(ReadValue(reader));

                            ReadTuple(reader);
                            int left = int.Parse(_tuple[0]);
                            int top = int.Parse(_tuple[1]);

                            ReadTuple(reader);
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

                            if (ReadTuple(reader) == 4)
                            { // split is optional
                                region.Splits = new int[] {int.Parse(_tuple[0]), int.Parse(_tuple[1]),
                                    int.Parse(_tuple[2]), int.Parse(_tuple[3])};

                                if (ReadTuple(reader) == 4)
                                { // pad is optional, but only present with splits
                                    region.Pads = new int[] {int.Parse(_tuple[0]), int.Parse(_tuple[1]),
                                        int.Parse(_tuple[2]), int.Parse(_tuple[3])};

                                    ReadTuple(reader);
                                }
                            }

                            region.OriginalWidth = int.Parse(_tuple[0]);
                            region.OriginalHeight = int.Parse(_tuple[1]);

                            ReadTuple(reader);
                            region.OffsetX = int.Parse(_tuple[0]);
                            region.OffsetY = int.Parse(_tuple[1]);

                            region.Index = int.Parse(ReadValue(reader));

                            if (flip) region.Flip = true;

                            _regions.Add(region);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new FormatException("Error reading pack file: " + stream, ex);
                }
            }
            _regions.Sort();

        }

    }
}