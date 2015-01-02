using System;
using NAudio.Wave;

namespace Astrid.Windows.Audio
{
    public class NAudioSoundEffectSampleProvider : ISampleProvider
    {
        public NAudioSoundEffectSampleProvider(NAudioSoundEffect soundEffect)
        {
            _soundEffect = soundEffect;
            _position = 0;
        }

        private readonly NAudioSoundEffect _soundEffect;
        private long _position;

        public int Read(float[] buffer, int offset, int count)
        {
            var availableSamples = _soundEffect.Data.Length - _position;
            var samplesToCopy = Math.Min(availableSamples, count);
            Array.Copy(_soundEffect.Data, _position, buffer, offset, samplesToCopy);
            _position += samplesToCopy;
            return (int)samplesToCopy;
        }

        public WaveFormat WaveFormat { get { return _soundEffect.WaveFormat; } }
    }
}