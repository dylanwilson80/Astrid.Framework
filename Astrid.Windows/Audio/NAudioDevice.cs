using System;
using Astrid.Framework.Assets;
using Astrid.Framework.Audio;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;

namespace Astrid.Windows.Audio
{
    // http://mark-dot-net.blogspot.co.uk/2014/02/fire-and-forget-audio-playback-with.html
    public class NAudioDevice : AudioDevice, IDisposable
    {
        public NAudioDevice(int sampleRate = 44100, int channels = 1)
        {
            // TODO: Throws exception if sound card not available
            _wavePlayer = new WaveOutEvent();
            _mixingSampleProvider = new MixingSampleProvider(WaveFormat.CreateIeeeFloatWaveFormat(sampleRate, channels))
            {
                ReadFully = true
            };
            _wavePlayer.Init(_mixingSampleProvider);
            _wavePlayer.Play();
        }

        private readonly IWavePlayer _wavePlayer;
        private readonly MixingSampleProvider _mixingSampleProvider;

        public override void Play(SoundEffect soundEffect)
        {
            var naudioSoundEffect = (NAudioSoundEffect) soundEffect;
            var sampleProvider = new NAudioSoundEffectSampleProvider(naudioSoundEffect);

            _mixingSampleProvider.AddMixerInput(sampleProvider);
        }
        
        public void Dispose()
        {
            _wavePlayer.Dispose();
        }
    }
}
