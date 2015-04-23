using System.Collections.Generic;
using Astrid.Core;
using Astrid.Gui;

namespace Astrid
{
    public interface IInputDeviceContext
    {
        Camera Camera { get; }
    }

    public abstract class InputDevice
    {
        private readonly IInputDeviceContext _context;

        protected InputDevice(IInputDeviceContext context)
        {
            _context = context;
            Processors = new List<InputProcessor>();
        }

        protected abstract Vector2 GetCurrentScreenCoordinates();

        public Vector2 Position 
        {
            get
            {
                var position = GetCurrentScreenCoordinates();
                return _context.Camera.ToWorldSpace(position); 
            }
        }

        public abstract bool IsTouching { get; }

        public abstract void UpdateState();
        public abstract bool IsKeyDown(Keys key);
        public abstract bool IsKeyUp(Keys key);

        public List<InputProcessor> Processors { get; private set; }

        public void Update()
        {
            UpdateState();

            // This is a for loop so that the collection can be modified during an update
            // ReSharper disable once ForCanBeConvertedToForeach
            for (var i = 0; i < Processors.Count; i++)
            {
                var inputProcessor = Processors[i];
                inputProcessor.Update(this);
            }
        }
    }
}
