using System.Collections.Generic;
using Astrid.Framework.Graphics;

namespace Astrid.Framework.Assets
{
    public class TextureAtlasGdxLoader : AssetLoader<TextureAtlas>
    {
        private TextureAtlas Load(AssetManager assetManager, TextureAtlasData data)
        {
            var atlas = new TextureAtlas("TODO");
            var textures = new List<Texture>();
            var pageToTexture = new Dictionary<TextureAtlasData.Page, Texture>();

            foreach (var page in data.Pages)
            {
                Texture texture;

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

            foreach (var region in data.Regions)
            {
                var texture = pageToTexture[region.Page];
                var width = region.Rotate ? region.Height : region.Width;
                var height = region.Rotate ? region.Width : region.Height;
                var left = region.Left;
                var top = region.Top;
                var atlasRegion = new TextureAtlasRegion(region.Name, texture, left, top, width, height)
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


        public override TextureAtlas Load(AssetManager assetManager, string assetPath)
        {
            using (var stream = assetManager.OpenStream(assetPath))
            {
                var data = TextureAtlasData.Load(stream, "", false);
                return Load(assetManager, data);
            }
        }
    }
}