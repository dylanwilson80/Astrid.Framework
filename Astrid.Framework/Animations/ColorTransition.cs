using System;
using Astrid.Core;

namespace Astrid.Animations
{
    public class ColorTransition : Transition<Color>
    {
        public ColorTransition(Func<Color> getValue, Action<Color> setValue, Color targetValue, TransitionParameters transitionParameters)
            : base(getValue, setValue, targetValue, transitionParameters)
        {
        }

        private int _changeInR;
        private int _changeInG;
        private int _changeInB;
        private int _changeInA;

        protected override Color CalculateNewValue(float multiplier)
        {
            var r = (byte)(InitialValue.R + _changeInR * multiplier);
            var g = (byte)(InitialValue.G + _changeInG * multiplier);
            var b = (byte)(InitialValue.B + _changeInB * multiplier);
            var a = (byte)(InitialValue.A + _changeInA * multiplier);
            return new Color(r, g, b, a);
        }

        protected override Color CalculateChangeInValue(Color initialValue, Color targetValue)
        {
            _changeInR = targetValue.R - InitialValue.R;
            _changeInG = targetValue.G - InitialValue.G;
            _changeInB = targetValue.B - InitialValue.B;
            _changeInA = targetValue.A - InitialValue.A;
            return new Color(_changeInR, _changeInG, _changeInB, _changeInA);
        }
    }
}