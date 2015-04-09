using System;

namespace Astrid
{
    public abstract class SoundEffect : IAsset, IEquatable<SoundEffect>, IDisposable
    {
        protected SoundEffect(string key, string name)
        {
            Key = key;
            Name = name;
        }

        public string Key { get; private set; }
        public string Name { get; private set; }
        
        public SoundEffectInstance Play()
        {
            return Play(1.0f);
        }

        public SoundEffectInstance Play(float volume)
        {
            var instance = CreateInstance();
            instance.Volume = volume;
            instance.Play();
            return instance;
        }

        public abstract SoundEffectInstance CreateInstance();
        
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

            return Key == other.Key;
        }

        public override int GetHashCode()
        {
            return Key.GetHashCode();
        }

        public abstract void Dispose();
    }
}
