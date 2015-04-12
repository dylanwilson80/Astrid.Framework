using NAudio.Wave;

namespace Astrid.Windows
{
    public class WindowsMusic : Music
    {
        public WindowsMusic(WindowsAudioDevice audioDevice, string filePath, string name) 
            : base(filePath, name)
        {
            _audioDevice = audioDevice;
            _audioFileReader = new AudioFileReader(filePath);
        }

        private readonly AudioFileReader _audioFileReader;
        private readonly WindowsAudioDevice _audioDevice;

        private float _volume = 1.0f;
        public override float Volume 
        {
            get { return _volume; }
            set 
            { 
                _volume = value;
                _audioFileReader.Volume = _volume;
            }
        }

        public override bool IsPlaying { get { return false; } }

        public override void Play()
        {
            if (_audioDevice.IsMusicEnabled)
                _audioDevice.AddMixerInput(_audioFileReader);
        }

        public override void Pause()
        {
            //if (_soundOut != null)
            //    _soundOut.Pause();
        }

        public override void Resume()
        {
            //if (_soundOut != null)
            //    _soundOut.Resume();
        }

        public override void Stop()
        {
            //if (_soundOut != null)
            //    _soundOut.Stop();
        }

        public override void Dispose()
        {
            //if (_soundOut != null)
            //    _soundOut.Dispose();

            //_waveSource.Dispose();
            _audioFileReader.Dispose();
        }
    }
}