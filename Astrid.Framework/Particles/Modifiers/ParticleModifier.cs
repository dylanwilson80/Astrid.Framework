using System.Collections.Generic;

namespace Astrid.Particles.Modifiers
{
    public abstract class ParticleModifier
    {
        public abstract void Apply(IEnumerable<Particle> particles, float deltaTime);
    }
}