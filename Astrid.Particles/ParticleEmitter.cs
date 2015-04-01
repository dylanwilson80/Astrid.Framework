using System.Collections.Generic;
using System.Linq;
using Astrid.Components.Components;
using Astrid.Core;
using Astrid.Framework;
using Astrid.Particles.Modifiers;

namespace Astrid.Particles
{
    public class ParticleEmitter : Drawable
    {
        public ParticleEmitter()
            : this(null, new ParticleEmitterParameters())
        {
        }

        public ParticleEmitter(TextureRegion textureRegion, ParticleEmitterParameters parameters)
        {
            _randomizer = new RangeRandom();
            _particles = new List<Particle>();

            TextureRegion = textureRegion;
            Parameters = parameters;
            Profile = new PointParticleEmitterProfile();
            Modifiers = new List<ParticleModifier>();
            AutoEmit = true;
            AutoEmitDelay = 0.1f;
        }

        private readonly RangeRandom _randomizer;
        private readonly List<Particle> _particles;

        public TextureRegion TextureRegion { get; set; }
        public ParticleEmitterParameters Parameters { get; private set; }
        public ParticleEmitterProfile Profile { get; set; }
        public List<ParticleModifier> Modifiers { get; private set; }
        public bool AutoEmit { get; set; }
        public float AutoEmitDelay { get; set; }

        public void Emit()
        {
            if (TextureRegion == null)
                return;

            var particleCount = _randomizer.GetInt(Parameters.Quantity);

            while (particleCount > 0)
            {
                var position = Entity.Position + Profile.GetOffset(_randomizer);
                var particle = new Particle() {Position = position};
                var speed = _randomizer.GetFloat(Parameters.Speed);

                particle.Velocity = Profile.GetHeading(_randomizer) * speed;
                particle.AngularVelocity = _randomizer.GetFloat(Parameters.AngularVelocity);
                particle.TimeToLive = _randomizer.GetFloat(Parameters.Lifetime);
                particle.Scale = _randomizer.GetFloat(Parameters.Scale) * Vector2.One;
                particle.Rotation = _randomizer.GetFloat(Parameters.Rotation);

                _particles.Add(particle);
                particleCount--;
            }
        }

        private float _autoEmitDelay;

        public void Update(float deltaTime)
        {
            if (AutoEmit)
            {
                _autoEmitDelay -= deltaTime;

                if (_autoEmitDelay <= 0)
                {
                    Emit();
                    _autoEmitDelay = AutoEmitDelay;
                }
            }

            foreach (var particle in _particles)
            {
                particle.TimeToLive -= deltaTime;
                particle.Position += particle.Velocity * deltaTime;
                particle.Rotation += particle.AngularVelocity * deltaTime;
                particle.Color = new Color(particle.Color, particle.TimeToLive > 1.0f ? 1.0f : particle.TimeToLive);
            }

            foreach (var particle in _particles.Where(p => p.TimeToLive <= 0).ToArray())
                _particles.Remove(particle);

            foreach (var particleModifier in Modifiers)
                particleModifier.Apply(_particles, deltaTime);
        }

        public override Rectangle GetBoundingRectangle()
        {
            return Rectangle.Empty;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            foreach (var particle in _particles)
                 spriteBatch.Draw(TextureRegion, particle.Position, particle.Color, particle.Origin, particle.Rotation, particle.Scale);
        }
    }
}