using System;
using Astrid.Core;

namespace Astrid.Gui
{
    public abstract class GuiControl : Sprite
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

        //public Color Color { get; set; }
        //public TextureRegion TextureRegion { get; set; }
        //public Vector2 Origin { get; set; }
        //public bool IsVisible { get; set; }
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

        public Rectangle GetBoundingRectangle()
        {
            if (TextureRegion != null)
                return new Rectangle((int)Position.X, (int)Position.Y, TextureRegion.Width, TextureRegion.Height);

            return Rectangle.Empty;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if(TextureRegion != null)
                spriteBatch.Draw(TextureRegion, Position, Color, Origin, Rotation, Scale);
        }
    }
}
