namespace RaiNa.IO
{
    public readonly partial struct Path
    {
        public string Name => System.IO.Path.GetFileName(Value.TrimEnd(
            System.IO.Path.DirectorySeparatorChar,
            System.IO.Path.AltDirectorySeparatorChar));

        public string FileName => System.IO.Path.GetFileName(Value);

        public string Extension => System.IO.Path.GetExtension(Value);

        public Path Parent
        {
            get
            {
                string directory = System.IO.Path.GetDirectoryName(Value);
                return string.IsNullOrEmpty(directory) ? Empty : new Path(directory);
            }
        }

        public bool IsEmpty => string.IsNullOrWhiteSpace(Value);

        public string FullPath => System.IO.Path.GetFullPath(Value);

        public bool HasExtension => !string.IsNullOrEmpty(Extension);

        public bool IsRooted => System.IO.Path.IsPathRooted(Value);
    }
}
