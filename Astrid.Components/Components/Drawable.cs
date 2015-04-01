using Astrid.Core;
using Astrid.Framework;

namespace Astrid.Components.Components
{
    public abstract class Drawable : Component
    {
        public abstract Rectangle GetBoundingRectangle();
        public abstract void Draw(SpriteBatch spriteBatch);
    }
}