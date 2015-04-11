
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
            WaveFormat = waveFormat;
        }

        private readonly WindowsAudioDevice _audioDevice;
        private readonly float[] _audioData;
        private long _position;

        public int Read(float[] buffer, int offset, int count)
        {
            var availableSamples = _audioData.Length - _position;
            var samplesToCopy = Math.Min(availableSamples, count);
            Array.Copy(_audioData, _position, buffer, offset, samplesToCopy);
            _position += samplesToCopy;
            return (int)samplesToCopy;
        }

        public WaveFormat WaveFormat { get; private set; }
        
        public override bool IsPlaying { get { throw new NotImplementedException(); } }
        //{
        //    get { return _soundOut.PlaybackState == PlaybackState.Playing; }
        //}

        public override float Volume { get; set; }
        //{
        //    get { return _soundOut.Volume; }
        //    set { _soundOut.Volume = value; }
        //}

        public override void Play()
        {
            if (_audioDevice.IsSoundEnabled)
                _audioDevice.AddMixerInput(this);
        }

        public override void Stop()
        {
            //if(IsPlaying)
            //    _soundOut.Stop();
        }

        public override void Pause()
        {
            //if(_soundOut.PlaybackState == PlaybackState.Playing)
            //    _soundOut.Pause();
        }

        public override void Dispose()
        {
            Stop();
            //_soundOut.Dispose();
        }
    }
}