namespace Astrid
{
    public class SoundEffectLoader : AssetLoader<SoundEffect>
    {
        public override SoundEffect Load(AssetManager assetManager, string assetPath)
        {
            return assetManager.LoadSoundEffect(assetPath);
        }
    }

    public class MusicLoader : AssetLoader<Music>
    {
        public override Music Load(AssetManager assetManager, string assetPath)
        {
            return assetManager.LoadMusic(assetPath);
        }
    }
}