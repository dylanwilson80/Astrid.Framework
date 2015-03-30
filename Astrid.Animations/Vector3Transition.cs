using System;
using Astrid.Core;

namespace Astrid.Animations
{
    public class Vector3Transition : Transition<Vector3>
    {
        public Vector3Transition(Func<Vector3> getValue, Action<Vector3> setValue, Vector3 targetValue, TransitionParameters transitionParameters)
            : base(getValue, setValue, targetValue, transitionParameters)
        {
        }

        protected override Vector3 CalculateNewValue(float multiplier)
        {
            return InitialValue + ChangeInValue * multiplier;
        }

        protected override Vector3 CalculateChangeInValue(Vector3 initialValue, Vector3 targetValue)
        {
            return TargetValue - InitialValue;
        }
    }
}