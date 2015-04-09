using Android.Media;

namespace Astrid.Android
{
    public class AndroidSoundEffect : SoundEffect
    {
        private readonly SoundPool _soundPool;
        private readonly int _soundId;

        public AndroidSoundEffect(string key, string name, SoundPool soundPool, int soundId) 
            : base(key, name)
        {
            _soundPool = soundPool;
            _soundId = soundId;
        }

        public override SoundEffectInstance CreateInstance()
        {
            return new AndroidSoundEffectInstance(_soundPool, _soundId);
        }

        public override void Dispose()
        {
            _soundPool.Unload(_soundId);
        }
    }
}