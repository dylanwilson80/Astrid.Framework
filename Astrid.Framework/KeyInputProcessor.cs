namespace Astrid
{
    public interface IKeyInputListener
    {
        bool OnKeyDown(Keys key);
        bool OnKeyUp(Keys key);
    }

    public class KeyInputProcessor : InputProcessor
    {
        private readonly IKeyInputListener _listener;
        private readonly Keys _key;
        private bool _isKeyDown;

        public KeyInputProcessor(IKeyInputListener listener, Keys key)
        {
            _listener = listener;
            _key = key;
        }

        public override void Update(InputDevice inputDevice)
        {
            if (_isKeyDown && inputDevice.IsKeyUp(_key))
            {
                _isKeyDown = false;
                _listener.OnKeyUp(_key);
            }

            if(!_isKeyDown && inputDevice.IsKeyDown(_key))
            {
                _isKeyDown = true;
                _listener.OnKeyDown(_key);
            }

        }
    }
}