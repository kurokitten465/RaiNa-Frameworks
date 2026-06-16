using System.Text.Json;

namespace RaiNa.IO.Internal
{
    internal class JsonUtility
    {
        private static readonly JsonSerializerOptions _options = new()
        {
            WriteIndented = true
        };

        public static string Serialize<T>(T value) => JsonSerializer.Serialize(value, _options);

        public static T Deserialize<T>(string json) => JsonSerializer.Deserialize<T>(json, _options);
    }
}
