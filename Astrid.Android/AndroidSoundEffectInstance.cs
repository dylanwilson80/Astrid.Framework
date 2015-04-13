using Android.Media;

namespace Astrid.Android
{
    public class AndroidSoundEffectInstance : SoundEffectInstance
    {
        private readonly SoundPool _soundPool;
        private readonly int _soundId;
        private int _streamId;

        public AndroidSoundEffectInstance(SoundPool soundPool, int soundId)
        {
            _soundPool = soundPool;
            _soundId = soundId;
            _playbackState = PlaybackState.Stopped;
        }

        private PlaybackState _playbackState;
        public override PlaybackState PlaybackState
        {
            get { return _playbackState; }
        }

        private float _volume = 1.0f;
        public override float Volume
        {
            get { return _volume; }
            set
            {
                _volume = value;
                _soundPool.SetVolume(_streamId, _volume, _volume);
            }
        }

        public override void Play()
        {
            _streamId = _soundPool.Play(_soundId, _volume, _volume, 0, 0, 1.0f);
            _playbackState = PlaybackState.Playing;
        }

        public override void Stop()
        {
            _soundPool.Stop(_streamId);
            _playbackState = PlaybackState.Stopped;
        }

        public override void Pause()
        {
            _soundPool.Pause(_streamId);
            _playbackState = PlaybackState.Paused;
        }

        public override void Dispose()
        {
            _playbackState = PlaybackState.Stopped;
            _soundPool.Unload(_soundId);
        }
    }
}