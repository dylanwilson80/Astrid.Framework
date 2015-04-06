using System;
using Newtonsoft.Json;

namespace Astrid
{
    public class JsonAssetConverter : JsonConverter
    {
        private readonly AssetManager _assetManager;

        public JsonAssetConverter(AssetManager assetManager)
        {
            _assetManager = assetManager;
        }
        
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value == null ) return;

            var type = value.GetType();

            if(type == typeof(TextureRegion))
            {
                var textureRegion = (TextureRegion) value;
                writer.WriteValue(textureRegion.Name);
            }
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (objectType == typeof(TextureRegion))
            {
                var textureRegionName = (string) reader.Value;
                return _assetManager.LoadTextureRegion(textureRegionName);
            }

            return null;
        }

        public override bool CanConvert(Type objectType)
        {
            if (objectType == typeof (TextureRegion))
                return true;

            return false;
        }
    }
}