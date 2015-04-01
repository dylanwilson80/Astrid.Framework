using System.Collections.Generic;

namespace Astrid.Particles.Modifiers
{
    public class DragParticleModifier : ParticleModifier
    {
        public DragParticleModifier()
        {
            DragCoefficient = 0.47f;
            Density = 0.5f;
        }

        public float DragCoefficient { get; set; }
        public float Density { get; set; }

        public override void Apply(IEnumerable<Particle> particles, float deltaTime)
        {
            foreach (var particle in particles)
            {
                var drag = -DragCoefficient * Density * particle.Mass * deltaTime;
                particle.Velocity += particle.Velocity * drag;
            }
        }
    }
}