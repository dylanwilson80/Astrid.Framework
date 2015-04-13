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
            _playbackState = PlaybackState.Stopped;
        }

        private readonly WindowsAudioDevice _audioDevice;
        private readonly AudioFileReader _audioFileReader;

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

        private PlaybackState _playbackState;
        public override PlaybackState PlaybackState 
        { 
            get { return _playbackState; } 
        }

        public override void Play()
        {
            if (_audioDevice.IsMusicEnabled)
            {
                if(_playbackState == PlaybackState.Stopped)
                    _audioDevice.AddMixerInput(this);

                _playbackState = PlaybackState.Playing;
            }
        }

        public override void Pause()
        {
            _playbackState = PlaybackState.Paused;
        }

        public override void Resume()
        {
            _playbackState = PlaybackState.Playing;
        }

        public override void Stop()
        {
            _audioFileReader.Position = 0;
            _playbackState = PlaybackState.Stopped;
        }

        public override void Dispose()
        {
            Stop();
            _audioFileReader.Dispose();
        }

        public int Read(float[] buffer, int offset, int count)
        {
            if (_playbackState == PlaybackState.Playing)
            {
                var sampleCount = _audioFileReader.Read(buffer, offset, count);

                if (sampleCount == 0)
                    Stop();

                return sampleCount;
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

        public WaveFormat WaveFormat 
        {
            get { return _audioFileReader.WaveFormat; }
        }
    }
}