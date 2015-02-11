using Astrid.Framework.Assets;

namespace Astrid.Framework.Graphics
{
    public class TextureAtlasRegion : TextureRegion
    {
        /*
         * When sprites are packed, if the original file name ends with a number, it is stored as the index and is not considered as
         * part of the sprite's name. This is useful for keeping animation frames in order.
         * @see TextureAtlas#findRegions(String) 
         */
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
        public float OffsetX { get; set; }

        /// <summary>
        /// The offset from the bottom of the original image to the bottom of the packed image, after whitespace was removed for packing.
        /// </summary>
        public float OffsetY { get; set; }

        /// <summary>
        /// The width of the image, after whitespace was removed for packing.
        /// </summary>
        public int PackedWidth { get; set; }

        /// <summary>
        /// The height of the image, after whitespace was removed for packing.
        /// </summary>
        public int PackedHeight { get; set; }

        /// <summary>
        /// The width of the image, before whitespace was removed and rotation was applied for packing.
        /// </summary>
        public int OriginalWidth { get; set; }

        /// <summary>
        /// The height of the image, before whitespace was removed and rotation was applied for packing.
        /// </summary>
        public int OriginalHeight { get; set; }

        /// <summary>
        /// If true, the region has been rotated 90 degrees counter clockwise.
        /// </summary>
        public bool Rotate { get; set; }

        /// <summary>
        /// The ninepatch splits, or null if not a ninepatch. Has 4 elements: left, right, top, bottom. Currently unused.
        /// </summary>
        public int[] Splits { get; set; }

        /// <summary>
        /// The ninepatch pads, or null if not a ninepatch or the has no padding. Has 4 elements: left, right, top, bottom. Currently unused.
        /// </summary>
        public int[] Pads { get; set; }

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
        public TextureAtlasRegion(string name, Texture texture, int x, int y, int width, int height)
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
        public TextureAtlasRegion(TextureAtlasRegion region)
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
            if (x) OffsetX = OriginalWidth - OffsetX - RotatedPackedWidth;
            if (y) OffsetY = OriginalHeight - OffsetY - RotatedPackedHeight;
        }

        /// <summary>
        /// Returns the packed width considering the rotate value; if it is true then it returns the packedHeight, otherwise it
        /// returns the packedWidth.
        /// </summary>
        /// <returns></returns>
        public float RotatedPackedWidth
        {
            get { return Rotate ? PackedHeight : PackedWidth; }
        }

        /// <summary>
        /// Returns the packed height considering the rotate value, if it is true then it returns the packedWidth, otherwise it
        /// returns the packedHeight.
        /// </summary>
        /// <returns></returns>
        public float RotatedPackedHeight
        {
            get { return Rotate ? PackedWidth : PackedHeight; }
        }

    }
}