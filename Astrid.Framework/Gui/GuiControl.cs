using System;
using Astrid.Core;

namespace Astrid.Gui
{
    public abstract class GuiControl : Drawable
    {
        protected GuiControl()
            : this(null)
        {
        }

        protected GuiControl(TextureRegion textureRegion)
            : base(textureRegion)
        {
            IsEnabled = true;
            Color = Color.White;
            IsVisible = true;
            Origin = new Vector2(0.5f, 0.5f);
        }

        public bool IsEnabled { get; set; }
        public bool IsTouching { get; private set; }

        public EventHandler Touched;
        public EventHandler Released;

        protected abstract void OnTouch(Rectangle shape, Vector2 touchPosition);
        protected abstract void OnRelease(Rectangle shape, Vector2 touchPosition);
        
        public virtual bool Update(float deltaTime, InputDevice inputDevice)
        {
            if (!IsEnabled)
                return false;

            var position = inputDevice.Position;
            var shape = GetBoundingRectangle();
            var isPreviouslyTouching = IsTouching;

            IsTouching = inputDevice.IsTouching && shape.Contains(position);

            if (!isPreviouslyTouching && IsTouching)
            {
                OnTouch(shape, position);
                Touched.Raise(this, EventArgs.Empty);
            }

            if (isPreviouslyTouching && !IsTouching)
            {
                OnRelease(shape, position);
                Released.Raise(this, EventArgs.Empty);
            }

            return true;
        }
    }
}
