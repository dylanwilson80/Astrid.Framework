using System;
using NAudio.Wave;

namespace Astrid.Windows
{
    public class WindowsSoundEffectInstance : SoundEffectInstance, ISampleProvider
    {
        public WindowsSoundEffectInstance(WindowsAudioDevice audioDevice, float[] audioData, WaveFormat waveFormat)
        {
            _audioDevice = audioDevice;
            _audioData = audioData;
            _playbackState = PlaybackState.Stopped;
            WaveFormat = waveFormat;
        }

        private readonly WindowsAudioDevice _audioDevice;
        private readonly float[] _audioData;
        private long _position;

        public int Read(float[] buffer, int offset, int count)
        {
            if (_playbackState == PlaybackState.Playing)
            {
                var availableSamples = _audioData.Length - _position;
                var samplesToCopy = Math.Min(availableSamples, count);
                Array.Copy(_audioData, _position, buffer, offset, samplesToCopy);
                _position += samplesToCopy;

                if (samplesToCopy == 0)
                    Stop();

                return (int) samplesToCopy;
            }
            
            if (_playbackState == PlaybackState.Paused)
            {
                var i = offset;

                while (i < offset + count)
                    buffer[i++] = 0.0f;

                return count;
            }

            _playbackState = PlaybackState.Stopped;
            return 0;
        }

        public WaveFormat WaveFormat { get; private set; }

        private PlaybackState _playbackState;
        public override PlaybackState PlaybackState
        {
            get { return _playbackState; }
        }

        public override float Volume { get; set; }

        public override void Play()
        {
            if (_audioDevice.IsSoundEnabled)
            {
                if(_playbackState == PlaybackState.Stopped)
                    _audioDevice.AddMixerInput(this);

                _playbackState = PlaybackState.Playing;
            }
        }

        public override void Stop()
        {
            _position = 0;
            _playbackState = PlaybackState.Stopped;
        }

        public override void Pause()
        {
            _playbackState = PlaybackState.Paused;
        }

        public override void Dispose()
        {
            Stop();
        }
    }
}