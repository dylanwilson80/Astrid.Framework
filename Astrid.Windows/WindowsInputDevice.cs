using Astrid.Core;
using OpenTK.Input;

namespace Astrid.Windows
{
    public class WindowsInputDevice : InputDevice
    {
        public WindowsInputDevice(IInputDeviceContext context) 
            : base(context)
        {
        }

        private MouseState _mouseState;
        private KeyboardState _keyboardState;

        private Vector2 _mousePosition;

        protected override Vector2 GetCurrentScreenCoordinates()
        {
            return _mousePosition;
        }

        public override void UpdateState()
        {
            _mouseState = Mouse.GetState();
            _keyboardState = Keyboard.GetState();
        }

        public override bool IsTouching
        {
            get { return _mouseState.IsButtonDown(MouseButton.Left); }
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

        internal void OnMouseMove(object sender, MouseMoveEventArgs e)
        {
            _mousePosition = new Vector2(e.X, e.Y);
        }
    }
}
