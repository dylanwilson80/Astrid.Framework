using CSCore.SoundOut;

namespace Astrid.Windows
{
    public class WindowsSoundEffectInstance : SoundEffectInstance
    {
        private readonly AudioDevice _audioDevice;
        private readonly ISoundOut _soundOut;

        public WindowsSoundEffectInstance(AudioDevice audioDevice, ISoundOut soundOut)
        {
            _audioDevice = audioDevice;
            _soundOut = soundOut;
        }

        public override bool IsPlaying
        {
            get { return _soundOut.PlaybackState == PlaybackState.Playing; }
        }

        public override float Volume 
        {
            get { return _soundOut.Volume; }
            set { _soundOut.Volume = value; }
        }

        public override void Play()
        {
            if(_audioDevice.IsSoundEnabled)
                _soundOut.Play();
        }

        public override void Stop()
        {
            if(IsPlaying)
                _soundOut.Stop();
        }

        public override void Pause()
        {
            if(_soundOut.PlaybackState == PlaybackState.Playing)
                _soundOut.Pause();
        }

        public override void Dispose()
        {
            Stop();
            _soundOut.Dispose();
        }
    }
}