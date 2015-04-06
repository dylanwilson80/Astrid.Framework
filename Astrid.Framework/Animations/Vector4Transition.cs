using System;
using Astrid.Core;

namespace Astrid.Animations
{
    public class Vector4Transition : Transition<Vector4>
    {
        public Vector4Transition(Func<Vector4> getValue, Action<Vector4> setValue, Vector4 targetValue, TransitionParameters transitionParameters)
            : base(getValue, setValue, targetValue, transitionParameters)
        {
        }

        protected override Vector4 CalculateNewValue(float multiplier)
        {
            return InitialValue + ChangeInValue * multiplier;
        }

        protected override Vector4 CalculateChangeInValue(Vector4 initialValue, Vector4 targetValue)
        {
            var w = targetValue.W - InitialValue.W;
            var x = targetValue.X - InitialValue.X;
            var y = targetValue.Y - InitialValue.Y;
            var z = targetValue.Z - InitialValue.Z;
            return new Vector4(w, x, y, z);
        }
    }
}