using System;
using Astrid.Framework.Audio;
using CSCore.SoundOut;

namespace Astrid.Windows.Audio
{
    public class CSCoreAudioDevice : AudioDevice, IDisposable
    {
        public CSCoreAudioDevice()
        {
        }

        public override void Play(SoundEffect soundEffect)
        {
            ((CSCoreSoundEffect) soundEffect).Play();
        }
        
        public void Dispose()
        {
        }
    }
}
