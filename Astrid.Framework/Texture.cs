using System;

namespace Astrid.Framework
{
    public abstract class Texture : IAsset, IEquatable<Texture>
    {
        protected Texture(int id, string filePath, string name, int width, int height)
        {
            Id = id;
            FilePath = filePath;
            Name = name;
            Width = width;
            Height = height;
        }

        public int Id { get; private set; }
        public string Name { get; private set; }
        public string FilePath { get; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }

        public override string ToString()
        {
            return string.Format("assetPath: {0} Name: {1} Width: {2} Height: {3}", Id, Name, Width, Height);
        }

        public static bool operator ==(Texture x, Texture y)
        {
            return Equals(x, y);
        }

        public static bool operator !=(Texture x, Texture y)
        {
            return !Equals(x, y);
        }

        public override bool Equals(object obj)
        {
            var other = obj as Texture;

            if (obj != null)
                return Equals(other);

            return false;
        }

        public bool Equals(Texture other)
        {
            if (ReferenceEquals(other, null)) 
                return false;

            return Id == other.Id;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
