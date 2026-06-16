namespace RaiNa.IO
{
    public readonly partial struct Path
    {
        public Path Combine(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
                return this;

            return new Path(System.IO.Path.Combine(Value, path));
        }

        public Path Combine(params string[] paths)
        {
            if (paths == null || paths.Length == 0)
                return this;

            string result = Value;

            foreach (string path in paths)
                result = System.IO.Path.Combine(result, path);

            return new(result);
        }

        public static Path operator / (Path left, string right) => left.Combine(right);
    }
}
