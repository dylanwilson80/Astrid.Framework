using System;

namespace Astrid.Framework.Animations
{
    public class IntAnimation : Animation<int>
    {
        public IntAnimation(int initialValue, int targetValue, Action<int> setValueAction, float duration) 
            : base(initialValue, targetValue, setValueAction, duration)
        {
            _changeInValue = targetValue - initialValue;
        }

        private readonly int _changeInValue;

        protected override int CalculateNewValue(float multiplier)
        {
            return (int)(InitialValue + _changeInValue * multiplier);
        }
    }
}