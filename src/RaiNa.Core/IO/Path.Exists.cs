namespace RaiNa.IO
{
    public readonly partial struct Path
    {
        public bool Exists() => FileExists() || DirectoryExists();

        public bool FileExists() => System.IO.File.Exists(Value);

        public bool DirectoryExists() => System.IO.Directory.Exists(Value);
    }
}
