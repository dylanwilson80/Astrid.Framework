using Astrid.Core;

namespace Astrid.Framework.Entities.Components.Particles
{
    public abstract class ParticleEmitterProfile
    {
        public abstract Vector2 GetOffset(RangeRandom randomizer);
        public abstract Vector2 GetHeading(RangeRandom randomizer);
    }

    public class TestParticleEmitterProfile : ParticleEmitterProfile
    {
        public Vector2 Offset { get; set; }
        public Vector2 Heading { get; set; }

        public override Vector2 GetOffset(RangeRandom randomizer)
        {
            return Offset;
        }

        public override Vector2 GetHeading(RangeRandom randomizer)
        {
            return Heading;
        }
    }

    public class PointParticleEmitterProfile : ParticleEmitterProfile
    {
        public override Vector2 GetOffset(RangeRandom randomizer)
        {
            return Vector2.Zero;
        }

        public override Vector2 GetHeading(RangeRandom randomizer)
        {
            return randomizer.GetUnitVector();
        }
    }
}