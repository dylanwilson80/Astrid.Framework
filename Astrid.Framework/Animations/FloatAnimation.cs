
using System;

namespace Astrid.Framework.Animations
{
    public class FloatAnimation : Animation<float>
    {
        public FloatAnimation(float initialValue, float targetValue, Action<float> setValue, float duration)
            : base(initialValue, targetValue, setValue, duration)
        {
            _changeInValue = targetValue - initialValue;
        }

        private readonly float _changeInValue;

        protected override float CalculateNewValue(float multiplier)
        {
            return InitialValue + _changeInValue * multiplier;
        }
    }
}