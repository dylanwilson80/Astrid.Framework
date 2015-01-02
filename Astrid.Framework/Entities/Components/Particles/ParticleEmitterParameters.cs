using Astrid.Core;

namespace Astrid.Framework.Entities.Components.Particles
{
    public class ParticleEmitterParameters
    {
        public ParticleEmitterParameters()
        {
            Quantity = new Range<int>(4, 8);
            Lifetime = new Range<float>(0.5f, 1.0f);
            Speed = new Range<float>(50f, 100f);
            AngularVelocity = new Range<float>(-5f, 5f);
            Scale = new Range<float>(0.5f, 1.0f);
            Rotation = new Range<float>(0, MathHelper.TwoPi);
        }

        public Range<int> Quantity { get; set; }
        public Range<float> Speed { get; set; }
        public Range<float> AngularVelocity { get; set; } 
        //public Range<Color> Color { get; set; }  // TODO: Particle color
        public Range<float> Scale { get; set; }
        public Range<float> Rotation { get; set; }
        public Range<float> Lifetime { get; set; }
    }
}