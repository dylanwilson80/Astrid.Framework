using System;

namespace Astrid
{
    public abstract class AudioDevice : IDisposable
    {
        protected AudioDevice()
        {
            IsSoundEnabled = true;
            IsMusicEnabled = true;
        }

        public bool IsSoundEnabled { get; set; }
        public bool IsMusicEnabled { get; set; }
        public abstract void Dispose();
    }
}
