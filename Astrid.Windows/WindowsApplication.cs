namespace Astrid.Windows
{
    public class WindowsApplication : ApplicationBase
    {
        public WindowsApplication(WindowsApplicationConfig config) 
        {
            _config = config;
        }

        private readonly WindowsApplicationConfig _config;
        
        public override AssetManager CreateAssetManager(IDeviceManager deviceManager)
        {
            return new WindowsAssetManager(deviceManager, _config.ContentPath);
        }

        private GLGraphicsDevice _graphicsDevice;
        public override GraphicsDevice CreateGraphicsDevice()
        {
            _graphicsDevice = new GLGraphicsDevice(_config.Width, _config.Height);
            return _graphicsDevice;
        }

        private WindowsInputDevice _inputDevice;
        public override InputDevice CreateInputDevice()
        {
            _inputDevice = new WindowsInputDevice();
            return _inputDevice;
        }

        private WindowsAudioDevice _audioDevice;
        public override AudioDevice CreateAudioDevice()
        {
            _audioDevice = new WindowsAudioDevice();
            return _audioDevice;
        }

        public override void Run(IApplicationListener game)
        {
            using (var gameWindow = new WindowsGameWindow(game, _graphicsDevice, _config))
            {
                gameWindow.MouseMove += _inputDevice.OnMouseMove;
                gameWindow.Run();
            }
        }
    }
}
