using System;
using Astrid.Core;

namespace Astrid.Gui
{
    public class GuiButton : GuiControl
    {
        //public GuiButton(Sprite sprite)
        //    : base(sprite)
        //{
        //}

        //public GuiButton(Sprite normalSprite, Sprite pressedSprite)
        //    : this(normalSprite, pressedSprite, null)
        //{
        //}

        //public GuiButton(Sprite normalSprite, Sprite pressedSprite, Sprite disabledSprite)
        //    : base(normalSprite, disabledSprite)
        //{
        //    PressedSprite = pressedSprite;
        //}

        public GuiButton(TextureRegion textureRegion)
        {
            TextureRegion = textureRegion;
        }

        public bool IsPressed { get; set; }
        public Sprite PressedSprite { get; set; }

        public event EventHandler Click;

        protected override Sprite GetSpriteForState()
        {
            if (IsPressed && PressedSprite != null)
                return PressedSprite;

            return null;
        }

        //public override string ToString()
        //{
        //    return string.Format("{0} {1}", GetCurrentSprite(), IsPressed);
        //}

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