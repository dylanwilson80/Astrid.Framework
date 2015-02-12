using System.Collections.Generic;

namespace Astrid.Framework.Assets.LibGDX
{
    public class GdxTextureAtlasLoader : AssetLoader<GdxTextureAtlas>
    {
        public override GdxTextureAtlas Load(AssetManager assetManager, string assetPath)
        {
            using (var stream = assetManager.OpenStream(assetPath))
            {
                var data = GdxTextureAtlasData.Load(assetPath, stream, string.Empty, false);
                var atlas = new GdxTextureAtlas(data.Name);
                var textures = new List<Texture>();
                var pageToTexture = new Dictionary<GdxTextureAtlasData.Page, Texture>();

                foreach (var page in data.Pages)
                {
                    var texture = page.Texture ?? assetManager.Load<Texture>(page.TextureHandle);
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
        }
    }
}