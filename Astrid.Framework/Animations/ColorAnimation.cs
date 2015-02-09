using System;
using Astrid.Core;

namespace Astrid.Framework.Animations
{
    public class ColorAnimation : Animation<Color>
    {
        public ColorAnimation(Color initialValue, Color targetValue, Action<Color> setValueAction, float duration)
            : base(initialValue, targetValue, setValueAction, duration)
        {
            _changeInR = targetValue.R - initialValue.R;
            _changeInG = targetValue.G - initialValue.G;
            _changeInB = targetValue.B - initialValue.B;
            _changeInA = targetValue.A - initialValue.A;
        }

        private readonly int _changeInR;
        private readonly int _changeInG;
        private readonly int _changeInB;
        private readonly int _changeInA;

        protected override Color CalculateNewValue(float multiplier)
        {
            var r = (byte)(InitialValue.R + _changeInR * multiplier);
            var g = (byte)(InitialValue.G + _changeInG * multiplier);
            var b = (byte)(InitialValue.B + _changeInB * multiplier);
            var a = (byte)(InitialValue.A + _changeInA * multiplier);
            return new Color(r, g, b, a);
        }
    }
}