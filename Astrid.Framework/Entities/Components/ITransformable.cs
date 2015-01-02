using Astrid.Core;

namespace Astrid.Framework.Entities.Components
{
    public interface IMovable
    {
        Vector2 Position { get; set; }
    }

    public interface IRotatable
    {
        float Rotation { get; set; }
    }

    public interface IScalable
    {
        Vector2 Scale { get; set; }
    }

    public interface ITransformable : IMovable, IRotatable, IScalable
    {
    }
}