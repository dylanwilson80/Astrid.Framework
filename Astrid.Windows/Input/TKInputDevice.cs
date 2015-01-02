using Astrid.Core;
using Astrid.Framework.Input;
using OpenTK.Input;


namespace Astrid.Windows.Input
{
    public class TKInputDevice : InputDevice
    {
        public TKInputDevice()
        {
        }

        private MouseState _mouseState;
        private KeyboardState _keyboardState;

        public override void Update()
        {
            _mouseState = Mouse.GetState();
            _keyboardState = Keyboard.GetState();
        }

        public override bool IsTouching()
        {
            return _mouseState.IsButtonDown(MouseButton.Left);
        }

        public override bool IsKeyDown(Keys key)
        {
            return _keyboardState.IsKeyDown((Key) key);
        }

        public override bool IsKeyUp(Keys key)
        {
            return _keyboardState.IsKeyDown((Key) key);
        }

        public bool IsKeyDown(Key key)
        {
            return _keyboardState.IsKeyDown(key);
        }

        private Vector2 _position;
        public override Vector2 GetPosition()
        {
            return _position;
        }

        // TODO: This is a bit icky
        internal void SetPosition(int x, int y)
        {
            _position = new Vector2(x, y);
        }
    }
}
