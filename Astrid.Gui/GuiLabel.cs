using System;
using Astrid.Core;
using Astrid.Framework;
using Astrid.Framework.Assets.Fonts;

namespace Astrid.Gui
{
    public enum HorizontalAlignment
    {
        Left, Right, Centre
    }

    public enum VerticalAlignment
    {
        Top, Bottom, Centre
    }

    public class GuiLabel : GuiControl
    {
        public GuiLabel(BitmapFont font, Sprite sprite) 
            : this(font, sprite, null)
        {
        }

        public GuiLabel(BitmapFont font, Sprite normalSprite, Sprite disabledSprite) 
            : base(normalSprite, disabledSprite)
        {
            _font = font;
            TextColor = Color.White;
            HorizontalAlignment = HorizontalAlignment.Centre;
            VerticalAlignment = VerticalAlignment.Centre;
        }
        
        private readonly BitmapFont _font;

        public string Text { get; set; }
        public Color TextColor { get; set; }
        public HorizontalAlignment HorizontalAlignment { get; set; }
        public VerticalAlignment VerticalAlignment { get; set; }

        protected override void OnTouch(Rectangle shape, Vector2 touchPosition)
        {
            
        }

        protected override void OnRelease(Rectangle shape, Vector2 touchPosition)
        {
            
        }

        protected override Sprite GetSpriteForState()
        {
            return null;
        }

        private int GetHorizontalPosition(int x, int width)
        {
            switch (HorizontalAlignment)
            {
                case HorizontalAlignment.Centre:
                    return x - width / 2;
                case HorizontalAlignment.Left:
                    return x;
                case HorizontalAlignment.Right:
                    return x - width;
            }

            throw new InvalidOperationException(string.Format("{0} is not supported", HorizontalAlignment));
        }

        private int GetVerticalPosition(int y, int height)
        {
            switch (VerticalAlignment)
            {
                case VerticalAlignment.Centre:
                    return y - height / 2;
                case VerticalAlignment.Top:
                    return y;
                case VerticalAlignment.Bottom:
                    return y - height;
            }

            throw new InvalidOperationException(string.Format("{0} is not supported", HorizontalAlignment));
        }


        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            var rectangle = _font.MeasureText(Text, 0, 0);
            var x = GetHorizontalPosition((int)Position.X, rectangle.Width);
            var y = GetVerticalPosition((int)Position.Y, rectangle.Height);

            _font.Draw(spriteBatch, Text, x, y, TextColor);
        }
    }
}