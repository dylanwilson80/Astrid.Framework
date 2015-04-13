using NAudio.Wave;

namespace Astrid.Windows
{
    public class WindowsMusic : Music, ISampleProvider
    {
        public WindowsMusic(WindowsAudioDevice audioDevice, string filePath, string name) 
            : base(filePath, name)
        {
            _audioDevice = audioDevice;
            _filePath = filePath;
            _audioFileReader = new AudioFileReader(filePath);
        }

        private readonly WindowsAudioDevice _audioDevice;
        private readonly string _filePath;
        private AudioFileReader _audioFileReader;

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

        public override void Play()
        {
            if (_audioDevice.IsMusicEnabled)
            {
                _audioDevice.AddMixerInput(this);
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
            //if (_isMixing)
            //{
            //    //_audioDevice.RemoveMixerInput(this);
            //    //_isMixing = false;
            //}

            //_audioFileReader.Dispose();
            //_audioFileReader = new AudioFileReader(_filePath);
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
                var sampleCount = _audioFileReader.Read(buffer, offset, count);

                if (sampleCount == 0)
                    Stop();

                return sampleCount;
            }

            return 0;
        }

        public WaveFormat WaveFormat 
        {
            get { return _audioFileReader.WaveFormat; }
        }
    }
}