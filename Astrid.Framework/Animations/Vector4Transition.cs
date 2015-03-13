using System;
using Astrid.Core;

namespace Astrid.Framework.Animations
{
    public class Vector4Transition : Transition<Vector4>
    {
        public Vector4Transition(Vector4 initialValue, Vector4 targetValue, Action<Vector4> setValueAction, float duration)
            : base(initialValue, targetValue, setValueAction, duration)
        {
            _changeInW = targetValue.W - initialValue.W;
            _changeInX = targetValue.X - initialValue.X;
            _changeInY = targetValue.Y - initialValue.Y;
            _changeInZ = targetValue.Z - initialValue.Z;
        }

        private readonly float _changeInW;
        private readonly float _changeInX;
        private readonly float _changeInY;
        private readonly float _changeInZ;

        protected override Vector4 CalculateNewValue(float multiplier)
        {
            var w = InitialValue.W + _changeInW * multiplier;
            var x = InitialValue.X + _changeInX * multiplier;
            var y = InitialValue.Y + _changeInY * multiplier;
            var z = InitialValue.Z + _changeInZ * multiplier;
            return new Vector4(w, x, y, z);
        }
    }
}