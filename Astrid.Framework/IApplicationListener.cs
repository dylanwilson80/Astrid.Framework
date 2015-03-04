using Astrid.Framework.Entities;

namespace Astrid.Framework
{
    public interface IApplicationListener : IDeviceManager
    {
        void Create();
        void Destroy();
        void Pause();
        void Resume();
        void Resize(int width, int height);
        void Update(float deltaTime);
        void Render(float deltaTime);
    }
}