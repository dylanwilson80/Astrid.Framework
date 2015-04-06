namespace Astrid
{
    // It's not exactly clear if we actually need this.
    public abstract class AudioDevice
    {
        protected AudioDevice()
        {
            IsSoundEnabled = true;
            IsMusicEnabled = true;
        }

        public bool IsSoundEnabled { get; set; }
        public bool IsMusicEnabled { get; set; }
    }
}
