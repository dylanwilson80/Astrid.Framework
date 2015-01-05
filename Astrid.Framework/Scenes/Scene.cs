using System.Collections.Generic;
using System.Linq;

namespace Astrid.Framework.Scenes
{
    public class Scene
    {
        public Scene()
        {
            Layers = new List<SceneLayer>();
        }

        public List<SceneLayer> Layers { get; private set; }

        public SceneLayer GetLayer(string name)
        {
            return Layers.FirstOrDefault(i => i.Name == name);
        }
    }
}