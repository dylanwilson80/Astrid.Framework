//using System.Collections.Generic;
//using Astrid.Core;
//using Astrid.Framework.Entities.Components;
//using FarseerPhysics;
//using FarseerPhysics.Dynamics;

//namespace Astrid.Framework.Entities.Systems
//{
//    public class PhysicsSystem : ComponentSystem<PhysicsComponent>
//    {
//        public PhysicsSystem(Vector2 gravity, float displayUnitsPerSimUnit = 60f)
//        {
//            ConvertUnits.SetDisplayUnitToSimUnitRatio(displayUnitsPerSimUnit);

//            _world = new World(gravity);
//        }

//        private readonly World _world;
//        private readonly List<PhysicsComponent> _physicsComponents = new List<PhysicsComponent>();

//        public override void Update(float deltaTime)
//        {
//            _world.Step(deltaTime);

//            foreach (var actor in _physicsComponents.ToArray())
//                actor.Update(deltaTime);
//        }

//        protected override void OnAttached(PhysicsComponent physicsComponent)
//        {
//            physicsComponent.Initialize(_world);
//            _physicsComponents.Add(physicsComponent);
//        }

//        protected override void OnDetached(PhysicsComponent physicsComponent)
//        {
//            physicsComponent.Dispose();
//            _physicsComponents.Remove(physicsComponent);
//        }
//    }
//}
