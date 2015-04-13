using System;
using Astrid.Core;

namespace Astrid
{
    public abstract class Music : IAsset, IEquatable<Music>, IDisposable
    {
        protected Music(string key, string name)
        {
            Key = key;
            Name = name;
        }

        public string Key { get; private set; }
        public string Name { get; private set; }

        public event EventHandler Stopped;

        protected void RaiseStoppedEvent()
        {
            Stopped.Raise(this, EventArgs.Empty);
        }
        
        public abstract float Volume { get; set; }
        public abstract PlaybackState PlaybackState { get; }
        public abstract void Play();
        public abstract void Pause();
        public abstract void Resume();
        public abstract void Stop();
        
        public static bool operator ==(Music x, Music y)
        {
            return Equals(x, y);
        }

        public static bool operator !=(Music x, Music y)
        {
            return !Equals(x, y);
        }

        public override bool Equals(object obj)
        {
            var other = obj as Music;

            if (obj != null)
                return Equals(other);

            return false;
        }

        public bool Equals(Music other)
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