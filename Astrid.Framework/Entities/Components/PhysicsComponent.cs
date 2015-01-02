using System;
using System.Collections.Generic;
using Astrid.Core;
using Astrid.Framework.Resources.Shapes;
using FarseerPhysics;
using FarseerPhysics.Dynamics;

namespace Astrid.Framework.Entities.Components
{
    public class PhysicsComponent : Component, IDisposable
    {
        public PhysicsComponent()
        {
            BodyType = BodyType.Dynamic;
            Shapes = new List<Shape>();
            Density = 1.0f;
        }

        public BodyType BodyType { get; set; }
        public List<Shape> Shapes { get; private set; }
        public float Density { get; set; }

        internal void Initialize(World world)
        {
            _body = new Body(world, ConvertUnits.ToSimUnits(Entity.Position), Entity.Rotation, BodyType, this);

            foreach (var shape in Shapes)
                _body.CreateFixture(shape.ToPhysicsShape(Density));
        }

        public Body _body;
        
        public void Dispose()
        {
            if (!_body.IsDisposed)
                _body.Dispose();
        }

        public void Update(float deltaTime)
        {
            if (Entity == null || _body == null || _body.IsStatic) return;

            if (_body.IsKinematic)
            {
                _body.Position = ConvertUnits.ToSimUnits(Entity.Position);
                _body.Rotation = Entity.Rotation;
            }
            else
            {
                Entity.Position = ConvertUnits.ToDisplayUnits(_body.Position);
                Entity.Rotation = _body.Rotation;
            }
        }
    }
}
