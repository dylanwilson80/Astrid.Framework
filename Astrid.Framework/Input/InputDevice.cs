using Astrid.Core;

namespace Astrid.Framework.Input
{
    public abstract class InputDevice
    {
        public abstract void Update();
        public abstract Vector2 GetPosition();
        public abstract bool IsTouching();
        public abstract bool IsKeyDown(Keys key);
        public abstract bool IsKeyUp(Keys key);
    }
}
