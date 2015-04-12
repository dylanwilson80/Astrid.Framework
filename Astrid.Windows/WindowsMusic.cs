using NAudio.Wave;

namespace Astrid.Windows
{
    public class WindowsMusic : Music, ISampleProvider
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

        private bool _isPlaying;
        public override bool IsPlaying 
        { 
            get { return _isPlaying; } 
        }

        private bool _isMixing;
        public override void Play()
        {
            if (_audioDevice.IsMusicEnabled)
            {
                if (!_isMixing)
                {
                    _audioDevice.AddMixerInput(this);
                    _isMixing = true;
                }

                _isPlaying = true;
            }
        }

        public override void Pause()
        {
            _isPlaying = false;
        }

        public override void Resume()
        {
            _isPlaying = true;
        }

        public override void Stop()
        {
            if (_isMixing)
            {
                _audioDevice.RemoveMixerInput(this);
                _isMixing = false;
            }

            _audioFileReader.Position = 0;
            _isPlaying = false;
        }

        public override void Dispose()
        {
            Stop();
            _audioFileReader.Dispose();
        }

        public int Read(float[] buffer, int offset, int count)
        {
            if (_isPlaying)
            {
                var bytes = _audioFileReader.Read(buffer, offset, count);

                if (bytes == 0)
                    Stop();

                return bytes;
            }

            return 0;
        }

        public WaveFormat WaveFormat 
        {
            get { return _audioFileReader.WaveFormat; }
        }
    }
}