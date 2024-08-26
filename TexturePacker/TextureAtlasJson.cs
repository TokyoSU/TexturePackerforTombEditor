using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace TexturePacker
{
    public struct AtlasData
    {
        [JsonProperty("name")]
        public string Name;
        [JsonProperty("x")]
        public int X;
        [JsonProperty("y")]
        public int Y;
        [JsonProperty("width")]
        public int Width;
        [JsonProperty("height")]
        public int Height;
    }

    public struct AtlasList
    {
        [JsonProperty("atlas")]
        public List<AtlasData> Atlas;
    }

    public class TextureAtlasJson
    {
        private AtlasList Json = new AtlasList();

        public void Initialize()
        {
            Json = new AtlasList
            {
                Atlas = new List<AtlasData>()
            };
        }

        public void Add(AtlasData data)
        {
            Json.Atlas.Add(data);
        }

        public void Clear()
        {
            Json.Atlas.Clear();
        }

        public void Save(string destPath)
        {
            (string path, string filename) = destPath.GetSeparatedPathAndFilename();
            var serializer = new JsonSerializer
            {
                NullValueHandling = NullValueHandling.Ignore,
                Formatting = Formatting.Indented
            };
            using (StreamWriter sw = new StreamWriter(path + "\\" + filename + ".json"))
            using (JsonWriter writer = new JsonTextWriter(sw))
                serializer.Serialize(writer, Json);
        }
    }
}
