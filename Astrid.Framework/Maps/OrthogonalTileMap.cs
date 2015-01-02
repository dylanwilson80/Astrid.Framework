using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Astrid.Core;
using Astrid.Framework.Assets;
using Astrid.Framework.Entities.Components;
using Astrid.Framework.Graphics;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Astrid.Framework.Maps
{
    //public class OrthogonalTileMap : Drawable, IAsset
    //{
    //    private OrthogonalTileMap(string name)
    //    {
    //        Name = name;
    //        Layers = new List<OrthogonalTileMapLayer>();
    //        TileSets = new List<OrthogonalTileMapTileSet>();
    //    }

    //    public int Width { get; private set; }
    //    public int Height { get; private set; }
    //    public int TileWidth { get; private set; }
    //    public int TileHeight { get; private set; }
    //    public List<OrthogonalTileMapLayer> Layers { get; private set; }
    //    public List<OrthogonalTileMapTileSet> TileSets { get; private set; } 
        
    //    public static OrthogonalTileMap Load(AssetManager assetManager, string jsonFile)
    //    {
    //        using (var stream = assetManager.OpenStream(jsonFile))
    //        using (var streamReader = new StreamReader(stream))
    //        using (var jsonReader = new JsonTextReader(streamReader))
    //        {
    //            var jsonObject = JObject.Load(jsonReader);
    //            var map = new OrthogonalTileMap(jsonFile)
    //            {
    //                Width = (int) jsonObject["width"],
    //                Height = (int) jsonObject["height"],
    //                TileWidth = (int) jsonObject["tilewidth"],
    //                TileHeight = (int) jsonObject["tileheight"]
    //            };

    //            var jsonLayers = (JArray)jsonObject["layers"];

    //            foreach (var jsonLayer in jsonLayers)
    //            {
    //                var dataArray = (JArray)jsonLayer["data"];
    //                var width = (int) jsonLayer["width"];
    //                var height = (int) jsonLayer["height"];
    //                var data = dataArray.Select(i => (int)i).ToArray();
    //                var layer = new OrthogonalTileMapLayer(width, height, data);
                    
    //                map.Layers.Add(layer);
    //            }

    //            var jsonTilesets = (JArray) jsonObject["tilesets"];

    //            foreach (var jsonTileset in jsonTilesets)
    //            {
    //                //var firstIndex = (int) jsonTileset["firstgid"];
    //                var name = (string)jsonTileset["name"];
    //                var image = (string) jsonTileset["image"];
    //                var texture = assetManager.Load<Texture>(image);
    //                var tileWidth = (int) jsonTileset["tilewidth"];
    //                var tileHeight = (int) jsonTileset["tileheight"];
    //                var tileSet = new OrthogonalTileMapTileSet(name, texture, tileWidth, tileHeight);

    //                map.TileSets.Add(tileSet);
    //            }

    //            return map;
    //        }
    //    }

    //    public override Rectangle GetBoundingRectangle()
    //    {
    //        // TODO: Calculate bounding rectangle
    //        return Rectangle.Empty;
    //    }

    //    public override void Draw(SpriteBatch spriteBatch)
    //    {
    //        var tileSet = TileSets[0];

    //        foreach (var layer in Layers)
    //        {
    //            var position = Vector2.Zero;

    //            for (var y = 0; y < layer.Height; y++)
    //            {
    //                position.X = 0;

    //                for (var x = 0; x < layer.Width; x++)
    //                {
    //                    var tileId = layer.GetTileAt(x, y);
    //                    var textureRegion = tileSet.GetTextureRegion(tileId);
                        
    //                    if(textureRegion != null)
    //                        spriteBatch.Draw(textureRegion, position);

    //                    position.X += TileWidth;
    //                }

    //                position.Y += TileWidth;
    //            }
    //        }
    //    }
    //}
}