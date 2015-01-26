using System.IO;
using Astrid.Framework.Assets;

namespace Astrid.Android
{
    public class AndroidAssetManager : AssetManager
    {
        public override Stream OpenStream(string path)
        {
            throw new System.NotImplementedException();
        }

        public override Texture LoadTexture(string assetPath)
        {
            throw new System.NotImplementedException();
        }

        public override SoundEffect LoadSoundEffect(string assetPath)
        {
            throw new System.NotImplementedException();
        }
    }
}