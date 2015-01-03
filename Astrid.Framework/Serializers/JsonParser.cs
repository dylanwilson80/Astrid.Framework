using System.IO;
using System.Runtime.Serialization.Formatters;
using Astrid.Framework.Assets;
using Astrid.Framework.Scenes;
using Newtonsoft.Json;

namespace Astrid.Framework.Serializers
{
    public class JsonParser
    {
        private readonly AssetManager _assetManager;

        public JsonParser(AssetManager assetManager)
        {
            _assetManager = assetManager;
        }

        public void SaveScene(Scene scene, StreamWriter writer)
        {
            var settings = CreateSerializerSettings();
            var json = JsonConvert.SerializeObject(scene, settings);
            writer.Write(json);
        }

        public Scene LoadScene(StreamReader reader)
        {
            var json = reader.ReadToEnd();
            var settings = CreateSerializerSettings();
            var scene = JsonConvert.DeserializeObject<Scene>(json, settings);
            return scene;
        }

        private JsonSerializerSettings CreateSerializerSettings()
        {
            var assetConverter = new JsonAssetConverter(_assetManager);
            var settings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                TypeNameHandling = TypeNameHandling.Auto,
                //TypeNameAssemblyFormat = FormatterAssemblyStyle.Simple,
                Binder = new JsonKnownComponentTypesSerializationBinder(),
            };
            settings.Converters.Add(assetConverter);
            return settings;
        }
    }
}
