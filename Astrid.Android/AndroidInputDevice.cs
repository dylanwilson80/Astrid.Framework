using Android.Views;
using Astrid.Core;

namespace Astrid.Android
{
    public class AndroidInputDevice : InputDevice
    {
        public AndroidInputDevice(IInputDeviceContext context) 
            : base(context)
        {
            _listener = new InputListener(this);
        }

        private Vector2 _position;

        protected override Vector2 GetCurrentScreenCoordinates()
        {
            return _position;
        }

        private bool _isTouching;
        public override bool IsTouching
        {
            get { return _isTouching; }
        }

        private readonly View.IOnTouchListener _listener;
        public View.IOnTouchListener Listener
        {
            get { return _listener; }
        }

        public override void UpdateState()
        {
        }

        public override bool IsKeyDown(Keys key)
        {
            return false;
        }

        public override bool IsKeyUp(Keys key)
        {
            return false;
        }

        private class InputListener : Java.Lang.Object, View.IOnTouchListener
        {
            private readonly AndroidInputDevice _parent;

            public InputListener(AndroidInputDevice parent)
            {
                _parent = parent;
            }

            public bool OnTouch(View v, MotionEvent e)
            {   
                switch (e.Action)
                {
                    case MotionEventActions.Down:
                        _parent._isTouching = true;
                        break;
                    case MotionEventActions.Move:
                        var pointerCoords = new MotionEvent.PointerCoords();
                        e.GetPointerCoords(0, pointerCoords);
                        _parent._position = new Vector2(pointerCoords.X, pointerCoords.Y);
                        break;
                    case MotionEventActions.Up:
                        _parent._isTouching = false;
                        break;
                    default:
                        return false;
                }

                // TODO: Not sure if this should be called here or somewhere else
                _parent.Update();
                return true;
            }
        }
    }
}