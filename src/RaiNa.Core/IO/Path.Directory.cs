using System.Collections.Generic;

namespace RaiNa.IO
{
    public readonly partial struct Path
    {
        public void CreateDirectory() => System.IO.Directory.CreateDirectory(Value);

        public void EnsureDirectory() => CreateDirectory();

        public void EnsureParentExists()
        {
            if (Parent == Empty)
                return;

            Parent.CreateDirectory();
        }

        public IEnumerable<Path> Files()
        {
            if (!DirectoryExists())
                yield break;

            foreach (string file in System.IO.Directory.EnumerateFiles(Value))
                yield return new Path(file);
        }

        public IEnumerable<Path> Directories()
        {
            if (!DirectoryExists())
                yield break;

            foreach (string directory in System.IO.Directory.EnumerateDirectories(Value))
                yield return new Path(directory);
        }

        public IEnumerable<Path> Children()
        {
            foreach (Path directory in Directories())
                yield return directory;

            foreach (Path file in Files())
                yield return file;
        }
    }
}
