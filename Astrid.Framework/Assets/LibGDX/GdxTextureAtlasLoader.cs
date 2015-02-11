using System.Collections.Generic;
using Astrid.Framework.Graphics;

namespace Astrid.Framework.Assets.LibGDX
{
    public class GdxTextureAtlasLoader : AssetLoader<GdxTextureAtlas>
    {
        private GdxTextureAtlas Load(AssetManager assetManager, GdxTextureAtlasData data)
        {
            var atlas = new GdxTextureAtlas("TODO");
            var textures = new List<Texture>();
            var pageToTexture = new Dictionary<GdxTextureAtlasData.Page, Texture>();

            foreach (var page in data.Pages)
            {
                Texture texture;

                if (page.Texture == null)
                {
                    texture = assetManager.Load<Texture>(page.TextureHandle);
                }
                else
                {
                    texture = page.Texture;
                }
                
                textures.Add(texture);
                pageToTexture.Add(page, texture);
            }

            foreach (var region in data.Regions)
            {
                var texture = pageToTexture[region.Page];
                var width = region.Rotate ? region.Height : region.Width;
                var height = region.Rotate ? region.Width : region.Height;
                var left = region.Left;
                var top = region.Top;
                var atlasRegion = new GdxTextureAtlasRegion(region.Name, texture, left, top, width, height)
                {
                    Index = region.Index,
                    OffsetX = region.OffsetX,
                    OffsetY = region.OffsetY,
                    OriginalHeight = region.OriginalHeight,
                    OriginalWidth = region.OriginalWidth,
                    Rotate = region.Rotate,
                    Splits = region.Splits,
                    Pads = region.Pads
                };
                
                if (region.Flip) 
                    atlasRegion.Flip(false, true);

                atlas.AddRegion(atlasRegion);
            }

            return atlas;
        }


        public override GdxTextureAtlas Load(AssetManager assetManager, string assetPath)
        {
            using (var stream = assetManager.OpenStream(assetPath))
            {
                var data = GdxTextureAtlasData.Load(stream, "", false);
                return Load(assetManager, data);
            }
        }
    }
}