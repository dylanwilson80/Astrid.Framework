using System;
using Astrid.Core;
using Astrid.Framework.Extensions;
using Astrid.Framework.Graphics;
using Astrid.Framework.Input;

namespace Astrid.Framework.Entities.Components.Gui
{
    public abstract class GuiControl : SceneNode // : Drawable
    {
        protected GuiControl(Sprite sprite)
            : this(sprite, null)
        {
        }

        protected GuiControl(Sprite normalSprite, Sprite disabledSprite)
        {
            IsEnabled = true;
            NormalSprite = normalSprite;
            DisabledSprite = disabledSprite;
        }

        public Sprite NormalSprite { get; set; }
        public Sprite DisabledSprite { get; set; }

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

        protected abstract Sprite GetSpriteForState();

        protected Sprite GetCurrentSprite()
        {
            if (!IsEnabled && DisabledSprite != null)
                return DisabledSprite;

            return GetSpriteForState() ?? NormalSprite;
        }

        public Rectangle GetBoundingRectangle()
        {
            var sprite = GetCurrentSprite();

            if (sprite != null)
                return sprite.GetBoundingRectangle(Position, Scale);

            return Rectangle.Empty;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            var sprite = GetCurrentSprite();

            if (sprite != null)
                spriteBatch.Draw(sprite, Position, Rotation, Scale);
        }
    }
}
