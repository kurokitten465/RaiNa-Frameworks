using System;

namespace RaiNa.IO
{
    public readonly partial struct Path : IEquatable<Path>
    {
        public string Value { get; }

        public Path(string path) => Value = string.IsNullOrEmpty(path) ? Empty : path;

        public static Path Empty => new(string.Empty);

        public static Path Current => new(Environment.CurrentDirectory);

        public static Path Temp => new(System.IO.Path.GetTempPath());

        public override string ToString() => Value;

        public bool Equals(Path other) => string.Equals(Value, other.Value, StringComparison.Ordinal);

        public override bool Equals(object obj) => obj is Path other && Equals(other);

        public override int GetHashCode() => Value.GetHashCode();

        public static bool operator == (Path left, Path right) => left.Equals(right);

        public static bool operator != (Path left, Path right) => !left.Equals(right);

        public static implicit operator string(Path path) => path.Value;

        public static implicit operator Path(string path) => new(path);
    }
}
