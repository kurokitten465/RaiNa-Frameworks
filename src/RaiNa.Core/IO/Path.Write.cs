using System.Collections.Generic;
using RaiNa.IO.Internal;

namespace RaiNa.IO
{
    public readonly partial struct Path
    {
        public void WriteText(string content)
        {
            EnsureParentExists();
            System.IO.File.WriteAllText(Value, content);
        }

        public void WriteLines(IEnumerable<string> lines)
        {
            EnsureParentExists();
            System.IO.File.WriteAllLines(Value, lines);
        }

        public void WriteBytes(byte[] bytes)
        {
            EnsureParentExists();
            System.IO.File.WriteAllBytes(Value, bytes);
        }

        public void WriteJson<T>(T value)
        {
            EnsureParentExists();
            string json = JsonUtility.Serialize(value);
            WriteText(json);
        }

        public void AppendText(string content)
        {
            EnsureParentExists();
            System.IO.File.AppendAllText(Value, content);
        }
    }
}
