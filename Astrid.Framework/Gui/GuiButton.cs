using System;
using Astrid.Core;

namespace Astrid.Gui
{
    public class GuiButton : GuiControl
    {
        public GuiButton(TextureRegion textureRegion)
        {
            TextureRegion = textureRegion;
        }

        public bool IsPressed { get; set; }
        public Sprite PressedSprite { get; set; }

        public event EventHandler Click;

        protected override void OnTouch(Rectangle shape, Vector2 touchPosition)
        {
            IsPressed = true;
        }

        protected override void OnRelease(Rectangle shape, Vector2 touchPosition)
        {
            IsPressed = false;

            if (shape.Contains(touchPosition))
                Click.Raise(this, EventArgs.Empty);
        }
    }
}