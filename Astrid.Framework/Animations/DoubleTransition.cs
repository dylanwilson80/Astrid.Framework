using System;

namespace Astrid.Framework.Animations
{
    public class DoubleTransition : Transition<double>
    {
        public DoubleTransition(Func<double> getValue, Action<double> setValue, double targetValue, TransitionParameters transitionParameters)
            : base(getValue, setValue, targetValue, transitionParameters)
        {
        }
        
        protected override double CalculateNewValue(float multiplier)
        {
            return InitialValue + ChangeInValue * multiplier;
        }

        protected override double CalculateChangeInValue(double initialValue, double targetValue)
        {
            return TargetValue - InitialValue;
        }
    }
}