using System.Text.Json;

namespace RaiNa.IO
{
    public readonly partial struct Path
    {
        public string ReadText() => System.IO.File.ReadAllText(Value);

        public string[] ReadLines() => System.IO.File.ReadAllLines(Value);

        public byte[] ReadBytes() => System.IO.File.ReadAllBytes(Value);

        public T ReadJson<T>()
        {
            string json = ReadText();
            return JsonSerializer.Deserialize<T>(json);
        }

        public bool TryReadText(out string text)
        {
            try
            {
                text = ReadText();
                return true;
            }
            catch
            {
                text = string.Empty;
                return false;
            }
        }

        public bool TryReadJson<T>(out T value)
        {
            try
            {
                value = ReadJson<T>();
                return true;
            }
            catch
            {
                value = default;
                return false;
            }
        }
    }
}