namespace Astrid.Maps
{
    //public class IsometricTileMap : Drawable
    //{
    //    public IsometricTileMap(TextureAtlas textureAtlas)
    //    {
    //        _tiles = textureAtlas.Regions.ToArray();

    //        _data = new int[_mapWidth, _mapHeight];
    //        _data[1, 1] = 1;
    //        _data[1, 2] = 1;
    //        _data[1, 3] = 1;
    //        _data[2, 2] = 1;

    //        _tileHalfWidth = _tileWidth / 2;
    //        _tileHalfHeight = _tileHeight / 2;
    //    }

    //    private const int _mapWidth = 9;
    //    private const int _mapHeight = 9;
    //    private const int _tileWidth = 100;
    //    private const int _tileHeight = 50;
    //    private readonly int[,] _data;
    //    private readonly int _tileHalfWidth;
    //    private readonly int _tileHalfHeight;
    //    private readonly TextureRegion[] _tiles;

    //    public override Rectangle GetBoundingRectangle()
    //    {
    //        // TODO: Calculate bounding rectangle
    //        return Rectangle.Empty;
    //    }

    //    public override void Draw(SpriteBatch spriteBatch)
    //    {
    //        var offset = new Vector2(-_tileHalfWidth, 0);
    //        var rowPosition = offset + new Vector2(_tileHalfWidth * _mapWidth, -_tileHalfHeight * _mapHeight);
    //        var columnPosition = rowPosition;

    //        for (var x = _mapWidth - 1; x >= 0; x--)
    //        {
    //            for (var y = 0; y < _mapHeight; y++)
    //            {
    //                var tileIndex = _data[x, y];
    //                spriteBatch.Draw(_tiles[tileIndex], rowPosition);

    //                rowPosition.X += _tileHalfWidth;
    //                rowPosition.Y += _tileHalfHeight;
    //            }

    //            columnPosition.X -= _tileHalfWidth;
    //            columnPosition.Y += _tileHalfHeight;
    //            rowPosition = columnPosition;
    //        }
    //    }
    //}
}
