using System.Collections.Generic;
using Astrid.Framework.Assets;
using Astrid.Framework.Scenes;

namespace Astrid.Framework.Entities
{
    public class EntityEngine
    {
        public EntityEngine(AssetManager assetManager, IComponentSystemFactory componentSystemFactory = null)
        {
            _assetManager = assetManager;
            _componentSystemFactory = componentSystemFactory;
            _spaces = new List<EntitySpace>();
        }

        private readonly AssetManager _assetManager;
        private readonly IComponentSystemFactory _componentSystemFactory;
        private readonly List<EntitySpace> _spaces;

        public EntitySpace CreateSpace(string name)
        {
            var space = new EntitySpace(name, _assetManager, _componentSystemFactory);
            _spaces.Add(space);
            return space;
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

        public Scene LoadScene(string assetPath)
        {
            var scene = _assetManager.LoadScene("Scene1.scene");
            var space = CreateSpace("Space1");

            foreach (var sceneNode in scene.Nodes)
            {
                var entity = space.CreateEntity(sceneNode.Name, sceneNode.Position, sceneNode.Rotation, sceneNode.Scale);

                foreach (var component in sceneNode.Components)
                    entity.Attach(component);
            }

            return scene;
        }
    }
}