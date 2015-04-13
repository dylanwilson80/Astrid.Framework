using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using System;

namespace Astrid.Windows
{
    public class WindowsAudioDevice : AudioDevice
    {
        public WindowsAudioDevice()
        {
            var waveFormat = WaveFormat.CreateIeeeFloatWaveFormat(_sampleRate, _channelCount);
            _wavePlayer = new WaveOutEvent();
            _mixingSampleProvider = new MixingSampleProvider(waveFormat)
            {
                ReadFully = true
            };
            _wavePlayer.Init(_mixingSampleProvider);
            _wavePlayer.Play();
        }

        private const int _sampleRate = 44100;
        private const int _channelCount = 2;

        private readonly IWavePlayer _wavePlayer;
        private readonly MixingSampleProvider _mixingSampleProvider;

        public void AddMixerInput(ISampleProvider sampleProvider)
        {
            sampleProvider = ConvertToRightChannelCount(sampleProvider);
            _mixingSampleProvider.AddMixerInput(sampleProvider);
        }

        public void RemoveMixerInput(ISampleProvider sampleProvider)
        {
            _mixingSampleProvider.RemoveMixerInput(sampleProvider);
        }

        private ISampleProvider ConvertToRightChannelCount(ISampleProvider sampleProvider)
        {
            if (sampleProvider.WaveFormat.Channels == _mixingSampleProvider.WaveFormat.Channels)
                return sampleProvider;

            if (sampleProvider.WaveFormat.Channels == 1 && _mixingSampleProvider.WaveFormat.Channels == 2)
                return new MonoToStereoSampleProvider(sampleProvider);

            throw new InvalidOperationException();
        }

        public override void Dispose()
        {
            _wavePlayer.Dispose();
        }
    }
}
