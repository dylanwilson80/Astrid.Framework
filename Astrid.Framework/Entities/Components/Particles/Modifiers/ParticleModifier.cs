using System.Collections.Generic;

namespace Astrid.Framework.Entities.Components.Particles.Modifiers
{
    public abstract class ParticleModifier
    {
        public abstract void Apply(IEnumerable<Particle> particles, float deltaTime);
    }
}