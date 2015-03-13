using System;

namespace Astrid.Framework.Animations
{
    public class DoubleTransition : Transition<double>
    {
        public DoubleTransition(double initialValue, double targetValue, Action<double> setValueAction, float duration) 
            : base(initialValue, targetValue, setValueAction, duration)
        {
            _changeInValue = targetValue - initialValue;
        }
        
        private readonly double _changeInValue;

        protected override double CalculateNewValue(float multiplier)
        {
            return InitialValue + _changeInValue * multiplier;
        }
    }
}