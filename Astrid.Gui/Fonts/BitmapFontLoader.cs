using System.Xml.Serialization;
using Astrid.Framework;

namespace Astrid.Gui.Fonts
{
    public class BitmapFontLoader : AssetLoader<BitmapFont>
    {
        public override BitmapFont Load(AssetManager assetManager, string assetPath)
        {
            using (var stream = assetManager.OpenStream(assetPath))
            {
                var deserializer = new XmlSerializer(typeof(FontFile));
                var fontFile = (FontFile)deserializer.Deserialize(stream);
                var texture = assetManager.Load<Texture>(fontFile.Pages[0].File);
                return new BitmapFont(assetPath, texture, fontFile);
            }
        }
    }
}
