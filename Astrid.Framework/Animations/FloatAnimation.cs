
using System;

namespace Astrid.Framework.Animations
{
    public class FloatAnimation : Animation<float>
    {
        public FloatAnimation(float initialValue, float targetValue, Action<float> setValue, float duration)
            : base(initialValue, targetValue, setValue, duration)
        {
            _setValue = setValue;
            ChangeInValue = TargetValue - InitialValue;
        }

        private readonly Action<float> _setValue;

        public float ChangeInValue { get; private set; }

        protected override void OnValueChanged(float multiplier)
        {
            var newValue = InitialValue + ChangeInValue * multiplier;
            _setValue(newValue);
        }
    }
}