using Astrid.Core;
using OpenTK.Graphics;

namespace Astrid.Windows.Graphics
{
    public static class ColorExtensions
    {
        public static Color4 ToColor4(this Color color)
        {
            return new Color4(color.R, color.G, color.B, color.A);
        }
    }
}
