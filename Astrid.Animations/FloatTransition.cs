
using System;

namespace Astrid.Animations
{
    public class FloatTransition : Transition<float>
    {
        public FloatTransition(Func<float> getValue, Action<float> setValue, float targetValue, TransitionParameters transitionParameters)
            : base(getValue, setValue, targetValue, transitionParameters)
        {
        }

        protected override float CalculateNewValue(float multiplier)
        {
            return InitialValue + ChangeInValue * multiplier;
        }

        protected override float CalculateChangeInValue(float initialValue, float targetValue)
        {
            return targetValue - initialValue;
        }
    }
}