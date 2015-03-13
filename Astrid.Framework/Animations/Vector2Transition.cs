using System;
using Astrid.Core;

namespace Astrid.Framework.Animations
{
    public class Vector2Transition : Transition<Vector2>
    {
        public Vector2Transition(Vector2 initialValue, Vector2 targetValue, Action<Vector2> setValueAction, float duration) 
            : base(initialValue, targetValue, setValueAction, duration)
        {
            _changeInX = targetValue.X - initialValue.X;
            _changeInY = targetValue.Y - initialValue.Y;
        }

        private readonly float _changeInX;
        private readonly float _changeInY;

        protected override Vector2 CalculateNewValue(float multiplier)
        {
            var x = InitialValue.X + _changeInX * multiplier;
            var y = InitialValue.Y + _changeInY * multiplier;
            return new Vector2(x, y);
        }
    }
}