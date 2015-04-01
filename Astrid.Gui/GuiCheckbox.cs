using System;
using Astrid.Core;
using Astrid.Framework;

namespace Astrid.Gui
{
    public class GuiCheckbox : GuiControl
    {
        public GuiCheckbox(Sprite normalSprite, Sprite checkedSprite)
            : base(normalSprite)
        {
            CheckedSprite = checkedSprite;
        }

        public Sprite CheckedSprite { get; set; }

        public bool IsChecked { get; set; }

        public event EventHandler CheckChanged;

        protected override void OnTouch(Rectangle shape, Vector2 touchPosition)
        {
        }

        protected override void OnRelease(Rectangle shape, Vector2 touchPosition)
        {
        }

        public override bool Update(float deltaTime, InputDevice inputDevice)
        {
            var previouslyPressed = IsTouching;

            if (base.Update(deltaTime, inputDevice))
            {
                if (!previouslyPressed && IsTouching)
                {
                    IsChecked = !IsChecked;
                    CheckChanged.Raise(this, EventArgs.Empty);
                }
            }

            return true;
        }

        protected override Sprite GetSpriteForState()
        {
            return IsChecked ? CheckedSprite : NormalSprite;
        }
    }
}
