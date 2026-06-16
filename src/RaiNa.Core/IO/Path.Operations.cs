using System;

namespace RaiNa.IO
{
    public readonly partial struct Path
    {
        public void Delete()
        {
            if (FileExists())
            {
                System.IO.File.Delete(Value);
                return;
            }

            if (DirectoryExists())
                System.IO.Directory.Delete(Value, recursive: true);
        }

        public void CopyTo(Path destination)
        {
            if (FileExists())
            {
                destination.EnsureParentExists();
                System.IO.File.Copy(Value, destination.Value, overwrite: true);
                return;
            }

            throw new InvalidOperationException($"Cannot copy '{Value}' because it is not a file.");
        }

        public void MoveTo(Path destination)
        {
            if (FileExists())
            {
                destination.EnsureParentExists();

                System.IO.File.Move(Value, destination.Value);
                return;
            }

            if (DirectoryExists())
            {
                System.IO.Directory.Move(Value, destination.Value);
                return;
            }

            throw new InvalidOperationException($"Cannot move '{Value}' because it does not exist.");
        }

        public Path Rename(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name cannot be null or whitespace.", nameof(name));

            Path destination = Parent / name;
            MoveTo(destination);
            return destination;
        }
    }
}
