using System;
using Astrid.Core;
using Astrid.Framework.Extensions;
using Astrid.Framework.Graphics;
using Astrid.Framework.Input;

namespace Astrid.Framework.Entities.Components.Gui
{
    public abstract class GuiControl : Drawable
    {
        protected GuiControl()
        {
            IsEnabled = true;
            NormalSprite = new Sprite();
            PressedSprite = new Sprite() { Scale = new Vector2(0.9f) };
            DisabledSprite = new Sprite() { Color = Color.Gray };
        }

        public Sprite NormalSprite { get; set; }
        public Sprite PressedSprite { get; set; }
        public Sprite DisabledSprite { get; set; }

        public bool IsEnabled { get; set; }
        public bool IsPressed { get; set; }

        public event EventHandler Pressed;
        public event EventHandler Released;
        public event EventHandler Click;

        public virtual bool Update(float deltaTime, InputDevice inputDevice)
        {
            if (!IsEnabled)
                return false;

            var position = inputDevice.Position;
            var shape = GetBoundingRectangle();
            var previouslyPressed = IsPressed;

            IsPressed = inputDevice.IsTouching && shape.Contains(position);

            if (!previouslyPressed && IsPressed)
                Pressed.Raise(this, EventArgs.Empty);

            if (previouslyPressed && !IsPressed)
            {
                Released.Raise(this, EventArgs.Empty);

                if (shape.Contains(position))
                    Click.Raise(this, EventArgs.Empty);
            }

            return true;
        }

        protected virtual Sprite GetCurrentSprite()
        {
            if (!IsEnabled && DisabledSprite != null)
                return DisabledSprite;

            if (IsPressed && PressedSprite != null)
                return PressedSprite;

            return NormalSprite;
        }

        public override Rectangle GetBoundingRectangle()
        {
            var sprite = GetCurrentSprite();

            if (sprite != null)
            {
                sprite.Entity = Entity;
                return sprite.GetBoundingRectangle();
            }

            return Rectangle.Empty;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            var sprite = GetCurrentSprite();

            if (sprite != null)
            {
                sprite.Entity = Entity;
                sprite.Draw(spriteBatch);
            }
        }
    }
}
