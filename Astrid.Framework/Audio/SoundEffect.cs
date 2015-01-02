using System;

namespace Astrid.Framework.Assets
{
    public abstract class SoundEffect : IAsset, IEquatable<SoundEffect>
    {
        protected SoundEffect(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; private set; }
        public string Name { get; private set; }

        public static bool operator ==(SoundEffect x, SoundEffect y)
        {
            return Equals(x, y);
        }

        public static bool operator !=(SoundEffect x, SoundEffect y)
        {
            return !Equals(x, y);
        }

        public override bool Equals(object obj)
        {
            var other = obj as SoundEffect;

            if (obj != null)
                return Equals(other);

            return false;
        }

        public bool Equals(SoundEffect other)
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
