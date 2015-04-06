namespace Astrid
{
    public class TextureLoader : AssetLoader<Texture>
    {
        public override Texture Load(AssetManager assetManager, string assetPath)
        {
            return assetManager.LoadTexture(assetPath);
        }
    }
}