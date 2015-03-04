using System;
using Astrid.Core;
using Astrid.Framework.Extensions;
using Astrid.Framework.Graphics;
using Astrid.Framework.Input;

namespace Astrid.Framework.Entities.Components.Gui
{
    public class GuiToggleButton : GuiControl
    {
        public GuiToggleButton()
        {
            CheckedSprite = new Sprite();
        }

        public Sprite CheckedSprite { get; set; }

        public bool IsChecked { get; set; }

        public event EventHandler CheckChanged;

        public override bool Update(float deltaTime, InputDevice inputDevice)
        {
            var previouslyPressed = IsPressed;

            if (base.Update(deltaTime, inputDevice))
            {
                if (!previouslyPressed && IsPressed)
                {
                    IsChecked = !IsChecked;
                    CheckChanged.Raise(this, EventArgs.Empty);
                }
            }

            return true;
        }

        protected override Sprite GetCurrentSprite()
        {
            if (!IsEnabled)
                return base.GetCurrentSprite();

            if (IsPressed) 
                return PressedSprite;

            return IsChecked ? CheckedSprite : base.GetCurrentSprite();
        }
    }
}