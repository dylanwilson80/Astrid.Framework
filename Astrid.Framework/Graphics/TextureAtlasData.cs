using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Astrid.Framework.Assets;

namespace Astrid.Framework.Graphics
{
    /// <summary>
    /// Provides TextureAtlasGDX with the ability to read texture atlas files in
    /// libGDX's format. This is not meant for everyday use.
    /// </summary>
    /// <see cref="TextureAtlas" />
    internal class TextureAtlasData
    {
        private TextureAtlasData()
        {
        }

        public static TextureAtlasData Load(Stream stream, string imageFolder, bool flip)
        {
            var data = new TextureAtlasData();

            using (var reader = new StreamReader(stream))
            {
                try
                {
                    Page pageImage = null;
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        if (line == null) break;
                        if (line.Trim().Length == 0)
                            pageImage = null;
                        else if (pageImage == null)
                        {
                            // TODO: This code might not work on all platforms
                            if (!string.IsNullOrEmpty(imageFolder) && !imageFolder.EndsWith("/"))
                                imageFolder += "/";

                            var textureHandle = imageFolder + line;

                            var width = 0f;
                            var height = 0f;

                            if (ReadTuple(reader) == 2)
                            { 
                                // size is only optional for an atlas packed with an old TexturePacker.
                                width = int.Parse(_tuple[0]);
                                height = int.Parse(_tuple[1]);
                                ReadTuple(reader);
                            }


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
                            pageImage = new Page(textureHandle, width, height, false);
                            data._pages.Add(pageImage);
                        }
                        else
                        {
                            var rotate = bool.Parse(ReadValue(reader));

                            ReadTuple(reader);
                            var left = int.Parse(_tuple[0]);
                            var top = int.Parse(_tuple[1]);

                            ReadTuple(reader);
                            var width = int.Parse(_tuple[0]);
                            var height = int.Parse(_tuple[1]);

                            var region = new Region
                            {
                                Page = pageImage,
                                Left = left,
                                Top = top,
                                Width = width,
                                Height = height,
                                Name = line,
                                Rotate = rotate
                            };

                            if (ReadTuple(reader) == 4)
                            { 
                                // split is optional
                                region.Splits = new[] 
                                {
                                    int.Parse(_tuple[0]), int.Parse(_tuple[1]),
                                    int.Parse(_tuple[2]), int.Parse(_tuple[3])
                                };

                                if (ReadTuple(reader) == 4)
                                { 
                                    // pad is optional, but only present with splits
                                    region.Pads = new[] 
                                    {
                                        int.Parse(_tuple[0]), int.Parse(_tuple[1]),
                                        int.Parse(_tuple[2]), int.Parse(_tuple[3])
                                    };

                                    ReadTuple(reader);
                                }
                            }

                            region.OriginalWidth = int.Parse(_tuple[0]);
                            region.OriginalHeight = int.Parse(_tuple[1]);

                            ReadTuple(reader);
                            region.OffsetX = int.Parse(_tuple[0]);
                            region.OffsetY = int.Parse(_tuple[1]);

                            region.Index = int.Parse(ReadValue(reader));

                            if (flip) 
                                region.Flip = true;

                            data._regions.Add(region);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new FormatException("Error reading pack file: " + stream, ex);
                }
            }
            data._regions.Sort();
            return data;
        }

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
            var line = reader.ReadLine();
            Debug.Assert(line != null, "line != null");
            var colon = line.IndexOf(':');
            if (colon == -1) throw new FormatException("Invalid line: " + line);
            int i, lastMatch = colon + 1;

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
        /// <see cref="TextureAtlas" />
        public class Page
        {
            public readonly string TextureHandle;
            public Texture Texture;
            public readonly float Width, Height;
            public readonly bool UseMipMaps;

            public Page(string textureHandle, float width, float height, bool useMipMaps)
            {
                Width = width;
                Height = height;
                TextureHandle = textureHandle;
                UseMipMaps = useMipMaps;
            }
        }
        /// <summary>
        /// An internally-used class that keeps information about a portion of a Page.
        /// This is not meant for everyday use.
        /// </summary>
        /// <see cref="TextureAtlas" />
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
                return string.Format("Region: {0} Index: {1}", Name, Index);
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
    }
}
