using System;
using System.Collections.Generic;
using System.Linq;

namespace Astrid.Components
{
    public class EntityEngine
    {
        public EntityEngine(AssetManager assetManager, ComponentSystemFactory componentSystemFactory = null)
        {
            _assetManager = assetManager;
            _componentSystemFactory = componentSystemFactory;
            _spaces = new List<EntitySpace>();
        }

        private readonly AssetManager _assetManager;
        private readonly ComponentSystemFactory _componentSystemFactory;
        private readonly List<EntitySpace> _spaces;

        public EntitySpace CreateSpace(string name)
        {
            var space = new EntitySpace(name, _componentSystemFactory);
            _spaces.Add(space);
            return space;
        }

        public void DestroySpace(string name)
        {
            var space = _spaces.FirstOrDefault(i => i.Name == name);

            if (space == null)
                throw new InvalidOperationException(string.Format("Space {0} does not exist", name));

            DestroySpace(space);
        }

        public void DestroySpace(EntitySpace space)
        {
            if (space == null)
                throw new ArgumentNullException("space");

            _spaces.Remove(space);
        }

        public void Update(float deltaTime)
        {
            foreach (var space in _spaces)
                space.Update(deltaTime);
        }

        public void Draw(float deltaTime)
        {
            foreach (var space in _spaces)
                space.Draw(deltaTime);
        }

        //public Scene LoadScene(string assetPath)
        //{
        //    var scene = _assetManager.LoadScene(assetPath);

        //    foreach (var layer in scene.Layers)
        //    {
        //        var space = CreateSpace(layer.Name);

        //        foreach (var sceneNode in layer.Sprites)
        //        {
        //            var entity = space.CreateEntity(sceneNode.Name, sceneNode.Position, sceneNode.Rotation, sceneNode.Scale);

        //            foreach (var component in sceneNode.Components)
        //                entity.Attach(component);
        //        }
        //    }
            
        //    return scene;
        //}
    }
}