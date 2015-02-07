using System.Linq;
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
            _instances = new ISoundOut[_maxInstances];
            _instanceIndex = 0;
        }

        private const int _maxInstances = 8;
        private int _instanceIndex;
        private readonly IWaveSource _waveSource;
        private readonly ISoundOut[] _instances;

        private static ISoundOut CreateInstance()
        {
            if (WasapiOut.IsSupportedOnCurrentPlatform)
                return new WasapiOut();

            return new DirectSoundOut();
        }
        
        public override void Dispose()
        {
            foreach (var instance in _instances.Where(instance => instance != null))
                instance.Dispose();

            _waveSource.Dispose();
        }

        public override int Play(float volume)
        {
            var currentInstance = _instances[_instanceIndex];

            if (currentInstance != null)
                currentInstance.Dispose();

            var instance = CreateInstance();
            instance.Initialize(_waveSource);
            instance.Volume = volume;
            instance.Play();
            

            _instances[_instanceIndex] = instance;
            _instanceIndex++;

            if (_instanceIndex == _maxInstances)
                _instanceIndex = 0;

            return _instanceIndex;
        }

        public override void Stop()
        {
            foreach (var instance in _instances)
                instance.Stop();
        }

        public override void Stop(int id)
        {
            if(_instances[id] != null)
                _instances[id].Stop();
        }

        public override void Pause()
        {
            foreach (var instance in _instances)
                instance.Pause();
        }

        public override void Pause(int id)
        {
            if (_instances[id] != null)
                _instances[id].Pause();
        }

        public override void Resume()
        {
            foreach (var instance in _instances)
                instance.Resume();
        }

        public override void Resume(int id)
        {
            if (_instances[id] != null)
                _instances[id].Resume();
        }
    }
}
