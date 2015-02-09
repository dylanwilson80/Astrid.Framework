using System;

namespace Astrid.Framework.Animations
{
    public abstract class Animation<T> : Animation
    {
        protected Animation(T initialValue, T targetValue, Action<float> setValue, float duration)
            : base(duration)
        {
            InitialValue = initialValue;
            TargetValue = targetValue;
            SetValue = setValue;
        }

        public T InitialValue { get; private set; }
        public T TargetValue { get; private set; }
        protected Action<float> SetValue { get; private set; }

        protected abstract void OnValueChanged(float multiplier);

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);
            OnValueChanged(CurrentValue);
        }
    }

    public class Animation
    {
        public Animation(float duration = 1.0f)
        {
            CurrentValue = 0.0f;
            Duration = duration;
            EasingFunction = EasingFunctions.Linear;
            IsPlaying = true;
        }

        public float CurrentTime { get; private set; }
        public float CurrentValue { get; private set; }
        public float Duration { get; private set; }
        public EasingFunction EasingFunction { get; set; }
        public bool IsPlaying { get; private set; }

        public virtual void Update(float deltaTime)
        {
            if (IsPlaying)
            {
                CurrentTime += deltaTime;
                CurrentValue = EasingFunction(CurrentTime/Duration);

                if (CurrentTime >= Duration)
                {

                    CurrentTime = Duration;
                    CurrentValue = EasingFunction(1.0f);
                    IsPlaying = false;
                }
            }
        }
    }
}
