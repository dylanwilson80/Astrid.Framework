using System.Collections.Generic;
using Astrid.Core;

namespace Astrid.Framework.Input
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

            foreach (var inputProcessor in Processors)
                inputProcessor.Update(this);
        }
    }
}
