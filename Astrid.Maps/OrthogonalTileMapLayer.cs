
namespace Astrid.Maps
{
    public class OrthogonalTileMapLayer
    {
        public OrthogonalTileMapLayer(int width, int height, int[] data)
        {
            Width = width;
            Height = height;
            _data = new int[width,height];

            var index = 0;
            for (var y = 0; y < Height; y++)
            {
                for (var x = 0; x < Width; x++)
                {
                    _data[x, y] = data[index];
                    index++;
                }
            }
        }

        private readonly int[,] _data;

        public int Width { get; private set; }
        public int Height { get; private set; }

        public int GetTileAt(int xIndex, int yIndex)
        {
            if (xIndex < 0 || xIndex > Width - 1)
                return 0;

            if (yIndex < 0 || yIndex > Height - 1)
                return 0;

            return _data[xIndex, yIndex];
        }
    }
}