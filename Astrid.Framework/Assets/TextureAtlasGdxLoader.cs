using System.Collections.Generic;
using Astrid.Framework.Graphics;

namespace Astrid.Framework.Assets
{
    public class TextureAtlasGdxLoader : AssetLoader<TextureAtlasGdx>
    {
        private TextureAtlasGdx Load(AssetManager assetManager, TextureAtlasData data)
        {
            var atlas = new TextureAtlasGdx("TODO");
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
                var width = region.Width;
                var height = region.Height;
                var atlasRegion = new AtlasRegion(region.Name, pageToTexture[region.Page], region.Left, region.Top,
                    region.Rotate ? height : width, region.Rotate ? width : height)
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


        public override TextureAtlasGdx Load(AssetManager assetManager, string assetPath)
        {
            throw new System.NotImplementedException();
        }
    }
}