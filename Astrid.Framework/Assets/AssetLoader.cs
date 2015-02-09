namespace Astrid.Framework.Assets
{
    public abstract class AssetLoader<T>
        where T : IAsset
    {
        public abstract T Load(AssetManager assetManager, string assetPath);
    }
}