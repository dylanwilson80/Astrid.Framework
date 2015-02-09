using System;

namespace Astrid.Framework.Animations
{
    public abstract class Animation<T> : Animation
    {
        protected Animation(T initialValue, T targetValue, Action<T> setValueAction, float duration)
            : base(duration)
        {
            InitialValue = initialValue;
            TargetValue = targetValue;
            _setValueAction = setValueAction;
        }

        public T InitialValue { get; private set; }
        public T TargetValue { get; private set; }

        private readonly Action<T> _setValueAction;

        protected abstract T CalculateNewValue(float multiplier);

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);

            var newValue = CalculateNewValue(CurrentValue);
            _setValueAction(newValue);
        }
    }

    public abstract class Animation
    {
        protected Animation(float duration)
        {
            CurrentValue = 0.0f;
            Duration = duration;
            EasingFunction = EasingFunctions.Linear;
            IsComplete = false;
            IsPaused = false;
        }

        public float CurrentTime { get; private set; }
        public float CurrentValue { get; private set; }
        public float Duration { get; private set; }
        public EasingFunction EasingFunction { get; set; }
        public bool IsComplete { get; private set; }
        public bool IsPaused { get; private set; }

        public void Pause()
        {
            IsPaused = true;
        }

        public void Resume()
        {
            IsPaused = false;
        }

        public void Stop()
        {
            IsComplete = true;
        }

        public virtual void Update(float deltaTime)
        {
            if (!IsComplete && !IsPaused)
            {
                CurrentTime += deltaTime;
                CurrentValue = EasingFunction(CurrentTime / Duration);

                if (CurrentTime >= Duration)
                {
                    CurrentTime = Duration;
                    CurrentValue = EasingFunction(1.0f);
                    IsComplete = true;
                }
            }
        }
    }
}
