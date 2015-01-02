using Astrid.Core;

namespace Astrid.Framework.Extensions
{
    public static class Vector2Extensions
    {
        public static Vector2 NormalizedCopy(this Vector2 vector2)
        {
            vector2.Normalize();
            return vector2;
        }
    }
}