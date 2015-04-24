namespace Astrid.Maps
{
    public class TiledMapLayer
    {
        public string Name { get; set; }
        public int[] Data { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public int Opacity { get; set; }
        public string Type { get; set; }
        public bool Visible { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
    }
}