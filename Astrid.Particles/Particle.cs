using Astrid.Core;

namespace Astrid.Particles
{
    public class Particle
    {
        public Particle()
        {
            Color = Color.White;
            Origin = new Vector2(0.5f, 0.5f);
            Scale = Vector2.One;
            Mass = 0.5f;
        }

        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }
        public float TimeToLive { get; set; }
        public Color Color { get; set; }
        public Vector2 Origin { get; set; }
        public Vector2 Scale { get; set; }
        public float Rotation { get; set; }
        public float AngularVelocity { get; set; }
        public float Mass { get; set; }
    }
}
