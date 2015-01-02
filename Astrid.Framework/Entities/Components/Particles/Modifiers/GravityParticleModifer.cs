using System.Collections.Generic;
using Astrid.Core;

namespace Astrid.Framework.Entities.Components.Particles.Modifiers
{
    public class GravityParticleModifer : ParticleModifier
    {
        public GravityParticleModifer()
        {
            Gravity = new Vector2(0, 100);
        }

        public Vector2 Gravity { get; set; }

        public override void Apply(IEnumerable<Particle> particles, float deltaTime)
        {
            foreach (var particle in particles)
                particle.Velocity += Gravity * deltaTime;
        }
    }
}