using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Astrid.Framework.Animations
{
    public class FloatAnimation : Animation
    {
        public FloatAnimation(float initialValue, float targetValue, float duration)
            : base(duration)
        {
            InitialValue = initialValue;
            TargetValue = targetValue;
            ChangeInValue = TargetValue - InitialValue;
        }

        public float InitialValue { get; private set; }
        public float TargetValue { get; private set; }
        public float ChangeInValue { get; private set; }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);


        }
    }

    public class Animation
    {
        public Animation(float duration = 1.0f)
        {
            CurrentValue = 0.0f;
            Duration = duration;
            EasingFunction = EasingFunctions.Linear;
        }

        public float CurrentValue { get; private set; }
        public float Duration { get; private set; }
        public EasingFunction EasingFunction { get; set; }

        public virtual void Update(float deltaTime)
        {
            CurrentValue = deltaTime / Duration;

            if (CurrentValue > 1.0f)
                CurrentValue = 1.0f;
        }
    }
}
