using Astrid.Core;

namespace Astrid.Framework
{
    public interface ITouchInputListener
    {
        bool OnTouchDown(Vector2 position, int pointerIndex);
        bool OnTouchUp(Vector2 position, int pointerIndex);
        bool OnTouchDrag(Vector2 position, Vector2 delta, int pointerIndex);
    }

    public class TouchInputProcessor : InputProcessor
    {
        private readonly ITouchInputListener _inputListener;

        public TouchInputProcessor(ITouchInputListener inputListener)
        {
            _inputListener = inputListener;
        }

        private bool _previouslyTouching = false;
        private Vector2 _previousPosition = Vector2.Zero;

        public override void Update(InputDevice inputDevice)
        {
            var position = inputDevice.Position;
            var isTouching = inputDevice.IsTouching;

            if (isTouching && !_previouslyTouching)
            {
                _previousPosition = position;
                _inputListener.OnTouchDown(position, 0);
            }

            if (isTouching)
            {
                var delta = position - _previousPosition;
                _inputListener.OnTouchDrag(position, delta, 0);
            }

            if (_previouslyTouching && !isTouching)
                _inputListener.OnTouchUp(position, 0);

            _previousPosition = position;
            _previouslyTouching = isTouching;
        }
    }
}