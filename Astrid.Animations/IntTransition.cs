using System;

namespace Astrid.Animations
{
    public class IntTransition : Transition<int>
    {
        public IntTransition(Func<int> getValue, Action<int> setValue, int targetValue, TransitionParameters transitionParameters)
            : base(getValue, setValue, targetValue, transitionParameters)
        {
        }

        protected override int CalculateNewValue(float multiplier)
        {
            return (int)(InitialValue + ChangeInValue * multiplier);
        }

        protected override int CalculateChangeInValue(int initialValue, int targetValue)
        {
            return targetValue - initialValue;
        }
    }
}
