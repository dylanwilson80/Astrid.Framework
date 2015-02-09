using System;
using Astrid.Core;

namespace Astrid.Framework.Animations
{
    public class Vector3Animation : Animation<Vector3>
    {
        public Vector3Animation(Vector3 initialValue, Vector3 targetValue, Action<Vector3> setValueAction, float duration)
            : base(initialValue, targetValue, setValueAction, duration)
        {
            _changeInX = targetValue.X - initialValue.X;
            _changeInY = targetValue.Y - initialValue.Y;
            _changeInZ = targetValue.Z - initialValue.Z;
        }

        private readonly float _changeInX;
        private readonly float _changeInY;
        private readonly float _changeInZ;

        protected override Vector3 CalculateNewValue(float multiplier)
        {
            var x = InitialValue.X + _changeInX * multiplier;
            var y = InitialValue.Y + _changeInY * multiplier;
            var z = InitialValue.Z + _changeInZ * multiplier;
            return new Vector3(x, y, z);
        }
    }
}