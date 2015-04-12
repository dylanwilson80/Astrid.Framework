using Android.Content.Res;
using Android.Media;

namespace Astrid.Android
{
    public class AndroidMusic : Music
    {
        public AndroidMusic(AssetFileDescriptor assetFileDescriptor, string name)
            : base(name, name)
        {
            _mediaPlayer = new MediaPlayer();
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

        public override bool IsPlaying
        {
            get { return _mediaPlayer.IsPlaying; }
        }

        public override void Play()
        {
            _mediaPlayer.Start();
        }

        public override void Pause()
        {
            _mediaPlayer.Pause();
        }

        public override void Resume()
        {
            _mediaPlayer.Start();
        }

        public override void Stop()
        {
            _mediaPlayer.Stop();
        }

        public override void Dispose()
        {
            _mediaPlayer.Release();
            _mediaPlayer.Dispose();
        }
    }
}