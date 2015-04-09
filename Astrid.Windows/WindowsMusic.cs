using System;
using CSCore;
using CSCore.Codecs;
using CSCore.SoundOut;

namespace Astrid.Windows
{
    public class WindowsMusic : Music
    {
        public WindowsMusic(AudioDevice audioDevice, string filePath, string name) 
            : base(filePath, name)
        {
            _audioDevice = audioDevice;
            _waveSource = CodecFactory.Instance.GetCodec(filePath);
            _soundOut = CreateSoundOut();
            _soundOut.Stopped += (sender, args) => RaiseStoppedEvent();
            _soundOut.Initialize(_waveSource);
            _soundOut.Volume = _volume;
        }

        private readonly AudioDevice _audioDevice;
        private readonly IWaveSource _waveSource;
        private readonly ISoundOut _soundOut;

        private static ISoundOut CreateSoundOut()
        {
            if (WasapiOut.IsSupportedOnCurrentPlatform)
                return new WasapiOut();

            return new DirectSoundOut();
        }

        private float _volume = 1.0f;
        public override float Volume 
        {
            get { return _volume; }
            set 
            { 
                _volume = value;

                if (_soundOut != null)
                    _soundOut.Volume = _volume;
            }
        }

        public override bool IsPlaying
        {
            get { return _soundOut != null && _soundOut.PlaybackState == PlaybackState.Playing; }
        }

        public override void Play()
        {
            if(_audioDevice.IsMusicEnabled)
                _soundOut.Play();
        }

        public override void Pause()
        {
            if (_soundOut != null)
                _soundOut.Pause();
        }

        public override void Resume()
        {
            if (_soundOut != null)
                _soundOut.Resume();
        }

        public override void Stop()
        {
            if (_soundOut != null)
                _soundOut.Stop();
        }

        public override void Dispose()
        {
            if (_soundOut != null)
                _soundOut.Dispose();

            _waveSource.Dispose();
        }
    }
}