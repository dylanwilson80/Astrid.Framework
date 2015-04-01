using System.Collections.Generic;
using Astrid.Core;

namespace Astrid.Framework
{
    public abstract class InputDevice
    {
        protected InputDevice()
        {
            Processors = new List<InputProcessor>();
        }

        public abstract Vector2 Position { get; }
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
