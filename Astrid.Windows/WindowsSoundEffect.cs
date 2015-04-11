

using System;
using System.Collections.Generic;
using System.Linq;
using NAudio.Wave;

namespace Astrid.Windows
{
    public class WindowsSoundEffect : SoundEffect
    {
        public WindowsSoundEffect(AudioDevice audioDevice, string filePath, string name)
            : base(filePath, name)
        {
            _audioDevice = audioDevice;
            _audioData = ReadAudioData(filePath, out _waveFormat);
        }

        private readonly float[] _audioData;
        private readonly WaveFormat _waveFormat;
        private readonly AudioDevice _audioDevice;

        private static float[] ReadAudioData(string filePath, out WaveFormat waveFormat)
        {
            using (var reader = new AudioFileReader(filePath))
            {
                waveFormat = reader.WaveFormat;
                var data = new List<float>((int)(reader.Length / 4));
                var buffer = new float[waveFormat.SampleRate * waveFormat.Channels];
                int samplesRead;

                while ((samplesRead = reader.Read(buffer, 0, buffer.Length)) > 0)
                {
                    data.AddRange(buffer.Take(samplesRead));
                }

                return data.ToArray();
            }
        }

        public override void Dispose()
        {
        }

        public override SoundEffectInstance CreateInstance()
        {
            var audioDevice = (WindowsAudioDevice) _audioDevice;
            return new WindowsSoundEffectInstance(audioDevice, _audioData, _waveFormat);
        }
    }
}
