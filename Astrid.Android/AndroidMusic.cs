using Android.Content.Res;
using Android.Media;

namespace Astrid.Android
{
    public class AndroidMusic : Music
    {
        public AndroidMusic(AssetFileDescriptor assetFileDescriptor, string name)
            : base(name, name)
        {
            _playbackState = PlaybackState.Stopped;
            _mediaPlayer = new MediaPlayer();
            _mediaPlayer.Completion += (sender, args) => _playbackState = PlaybackState.Stopped;
            _mediaPlayer.Reset();
            _mediaPlayer.SetDataSource(assetFileDescriptor.FileDescriptor, assetFileDescriptor.StartOffset, assetFileDescriptor.Length);
            _mediaPlayer.SetAudioStreamType(Stream.Music);
            _mediaPlayer.Prepare();
        }

        private readonly MediaPlayer _mediaPlayer;

        private float _volume = 1.0f;
        public override float Volume
        {
            get { return _volume; }
            set
            {
                _volume = value;
                _mediaPlayer.SetVolume(_volume, _volume);
            }
        }

        private PlaybackState _playbackState;
        public override PlaybackState PlaybackState
        {
            get { return _playbackState; }
        }

        public override void Play()
        {
            _mediaPlayer.Start();
            _playbackState = PlaybackState.Playing;
        }

        public override void Pause()
        {
            _mediaPlayer.Pause();
            _playbackState = PlaybackState.Paused;
        }

        public override void Resume()
        {
            _mediaPlayer.Start();
            _playbackState = PlaybackState.Playing;
        }

        public override void Stop()
        {
            _mediaPlayer.Stop();
            _playbackState = PlaybackState.Stopped;
        }

        public override void Dispose()
        {
            Stop();
            _mediaPlayer.Release();
            _mediaPlayer.Dispose();
        }
    }
}