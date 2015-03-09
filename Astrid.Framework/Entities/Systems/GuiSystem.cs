using System.Collections.Generic;
using Astrid.Framework.Input;

namespace Astrid.Framework.Entities.Systems
{
    //public class GuiSystem : ComponentSystem<GuiControl>
    //{
    //    public GuiSystem(InputDevice inputDevice)
    //    {
    //        _inputDevice = inputDevice;
    //        _controls = new List<GuiControl>();
    //    }

    //    private readonly InputDevice _inputDevice;
    //    private readonly List<GuiControl> _controls;

    //    public override void Update(float deltaTime)
    //    {
    //        foreach (var control in _controls.ToArray())
    //            control.Update(deltaTime, _inputDevice);
    //    }

    //    protected override void OnAttached(GuiControl component)
    //    {
    //        _controls.Add(component);
    //    }

    //    protected override void OnDetached(GuiControl component)
    //    {
    //        _controls.Remove(component);
    //    }
    //}
}