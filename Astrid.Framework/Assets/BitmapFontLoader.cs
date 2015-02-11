using System.IO;
using Astrid.Framework.Assets.BMFonts.TODO;

namespace Astrid.Framework.Assets
{
    public class BitmapFont : IAsset
    {
        public BitmapFont(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }
    }

    public class BitmapFontLoader : AssetLoader<BitmapFont>
    {
        public override BitmapFont Load(AssetManager assetManager, string assetPath)
        {
            using (var stream = assetManager.OpenStream(assetPath))
            using (var reader = new StreamReader(stream))
            {
                //while (!reader.EndOfStream)
                //{
                //    var line = reader.ReadLine();

                //}

                var fontFile = FontLoader.Load(stream);
            }

            return new BitmapFont(assetPath);
        }
    }
}
