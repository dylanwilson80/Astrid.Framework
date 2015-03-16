using System;
using Astrid.Core;

namespace Astrid.Framework.Animations
{
    public class Vector2Transition : Transition<Vector2>
    {
        public Vector2Transition(Func<Vector2> getValue, Action<Vector2> setValue, Vector2 targetValue, TransitionParameters parameters)
            : base(getValue, setValue, targetValue, parameters)
        {
        }

        protected override Vector2 CalculateNewValue(float multiplier)
        {
            return InitialValue + ChangeInValue * multiplier;
        }

        protected override Vector2 CalculateChangeInValue(Vector2 initialValue, Vector2 targetValue)
        {
            return TargetValue - InitialValue;
        }
    }
}