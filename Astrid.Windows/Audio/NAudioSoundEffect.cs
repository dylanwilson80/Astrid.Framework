using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Astrid.Framework.Assets;
using NAudio.Wave;

namespace Astrid.Windows.Audio
{
    public class NAudioSoundEffect : SoundEffect
    {
        private NAudioSoundEffect(int id, string name, string filePath) 
            : base(id, name)
        {
            FilePath = filePath;

            // http://mark-dot-net.blogspot.co.uk/2014/02/fire-and-forget-audio-playback-with.html
            using (var audioFileReader = new AudioFileReader(filePath))
            {
                WaveFormat = audioFileReader.WaveFormat;

                var wholeFile = new List<float>((int)(audioFileReader.Length / 4));
                var readBuffer = new float[audioFileReader.WaveFormat.SampleRate * audioFileReader.WaveFormat.Channels];
                int samplesRead;

                while ((samplesRead = audioFileReader.Read(readBuffer, 0, readBuffer.Length)) > 0)
                {
                    wholeFile.AddRange(readBuffer.Take(samplesRead));
                }
                Data = wholeFile.ToArray();
            }
        }

        private static int _nextId;

        public string FilePath { get; private set; }
        public WaveFormat WaveFormat { get; private set; }
        public float[] Data { get; private set; }

        public static NAudioSoundEffect Load(string filePath)
        {
            _nextId++;
            return new NAudioSoundEffect(_nextId, Path.GetFileNameWithoutExtension(filePath), filePath);
        }
    }
}
