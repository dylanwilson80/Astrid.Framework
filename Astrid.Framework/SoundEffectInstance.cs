using System;

namespace Astrid
{
    public abstract class SoundEffectInstance : IDisposable
    {
        public abstract bool IsPlaying { get; }
        public abstract float Volume { get; set; }

        public abstract void Play();
        public abstract void Stop();
        public abstract void Pause();
        public abstract void Dispose();
    }
}