using System.Collections.Generic;
using Astrid.Framework.Entities.Systems;

namespace Astrid.Framework.Entities.Components.Particles
{
    public class ParticleSystem : ComponentSystem<ParticleEmitter>
    {
        public ParticleSystem()
        {
            _emitters = new List<ParticleEmitter>();
            _particles = new List<Particle>();
        }

        private readonly DrawableSystem _drawableSystem;
        private readonly List<ParticleEmitter> _emitters;
        private readonly List<Particle> _particles; 

        public override void Update(float deltaTime)
        {
            foreach (var particleEmitter in _emitters.ToArray())
                particleEmitter.Update(deltaTime);
        }

        protected override void OnAttached(ParticleEmitter component)
        {
            _emitters.Add(component);
        }

        protected override void OnDetached(ParticleEmitter component)
        {
            _emitters.Remove(component);
        }
    }
}