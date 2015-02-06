using Astrid.Framework.Audio;
using CSCore;
using CSCore.Codecs;
using CSCore.SoundOut;

namespace Astrid.Windows.Audio
{
    public class CSCoreSoundEffect : SoundEffect
    {
        public CSCoreSoundEffect(string filePath, string name)
            : base(filePath, name)
        {
            _waveSource = CodecFactory.Instance.GetCodec(filePath);
        }
        
        private readonly IWaveSource _waveSource;
        private ISoundOut _soundOut;

        private static ISoundOut GetSoundOut()
        {
            if (WasapiOut.IsSupportedOnCurrentPlatform)
                return new WasapiOut();

            return new DirectSoundOut();
        }
        
        public override void Dispose()
        {
            _waveSource.Dispose();
            _soundOut.Dispose();
        }

        public void Play()
        {
            if (_soundOut == null) 
                _soundOut = GetSoundOut();

            if(_soundOut.PlaybackState == PlaybackState.Playing) 
                _soundOut.Stop();

            _soundOut.Initialize(_waveSource);
            _soundOut.Play();
        }
    }
}
