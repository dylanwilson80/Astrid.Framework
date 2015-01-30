using Astrid.Core;

namespace Astrid.Framework.Input
{
    public abstract class InputDevice
    {
        public abstract Vector2 Position { get; }
        public abstract bool IsTouching { get; }

        public abstract void Update();
        public abstract bool IsKeyDown(Keys key);
        public abstract bool IsKeyUp(Keys key);
    }
}
