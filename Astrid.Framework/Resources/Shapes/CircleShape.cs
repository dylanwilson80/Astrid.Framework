//using FarseerPhysics;

//namespace Astrid.Framework.Resources.Shapes
//{
//    public class CircleShape : Shape
//    {
//        public CircleShape()
//            : this(0)
//        {
//        }

//        public CircleShape(float radius)
//        {
//            Radius = radius;
//        }

//        public float Radius { get; private set; }
        
//        public override FarseerPhysics.Collision.Shapes.Shape ToPhysicsShape(float density)
//        {
//            var radius = ConvertUnits.ToSimUnits(Radius);

//            return new FarseerPhysics.Collision.Shapes.CircleShape(radius, density);
//        }
//    }
//}