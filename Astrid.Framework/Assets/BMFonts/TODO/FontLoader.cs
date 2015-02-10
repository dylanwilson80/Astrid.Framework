using System.IO;
using System.Xml.Serialization;

namespace CraftworkGames.Framework.Components.Gui.Fonts.BmFontXmlSerializer
{
	// ---- AngelCode BmFont XML serializer ----------------------
	// ---- By DeadlyDan @ deadlydan@gmail.com -------------------
	// ---- There's no license restrictions, use as you will. ----
	// ---- Credits to http://www.angelcode.com/ -----------------
	public class FontLoader
	{
        public static FontFile Load(Stream stream)
        {
            var deserializer = new XmlSerializer(typeof(FontFile));
            var file = (FontFile)deserializer.Deserialize(stream);
            return file;
        }
	}
}
