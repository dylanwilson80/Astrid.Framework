namespace Astrid.Framework
{
    public class SoundEffectLoader : AssetLoader<SoundEffect>
    {
        public override SoundEffect Load(AssetManager assetManager, string assetPath)
        {
            return assetManager.LoadSoundEffect(assetPath);
        }
    }
}