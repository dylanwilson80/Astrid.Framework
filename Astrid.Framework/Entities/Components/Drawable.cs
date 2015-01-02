using Astrid.Core;
using Astrid.Framework.Graphics;

namespace Astrid.Framework.Entities.Components
{
    public abstract class Drawable : Component
    {
        public abstract Rectangle GetBoundingRectangle();
        public abstract void Draw(SpriteBatch spriteBatch);
    }
}