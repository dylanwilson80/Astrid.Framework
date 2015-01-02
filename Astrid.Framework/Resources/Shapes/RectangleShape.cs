using FarseerPhysics;
using FarseerPhysics.Common;

namespace Astrid.Framework.Resources.Shapes
{
    public class RectangleShape : Shape
    {
        public RectangleShape()
            : this(0, 0)
        {
        }

        public RectangleShape(float width, float height)
        {
            Width = width;
            Height = height;
        }

        public float Width { get; private set; }
        public float Height { get; private set; }

        public override FarseerPhysics.Collision.Shapes.Shape ToPhysicsShape(float density)
        {
            var halfWidth = ConvertUnits.ToSimUnits(Width)*0.5f;
            var halfHeight = ConvertUnits.ToSimUnits(Height)*0.5f;
            var vertices = PolygonTools.CreateRectangle(halfWidth, halfHeight);

            return new FarseerPhysics.Collision.Shapes.PolygonShape(vertices, density);
        }
    }
}