using System;
using Astrid.Core;

namespace Astrid.Animations
{
    public abstract class Transition<T> : Transition
    {
        protected Transition(Func<T> getValue, Action<T> setValue, T targetValue, TransitionParameters transitionParameters)
            : base(transitionParameters)
        {
            _getValue = getValue;
            _setValue = setValue;
            TargetValue = targetValue;
            Reset();
        }

        private readonly Action<T> _setValue;
        private readonly Func<T> _getValue; 

        public T InitialValue { get; private set; }
        public T TargetValue { get; private set; }
        public T ChangeInValue { get; private set; }

        protected abstract T CalculateNewValue(float multiplier);
        protected abstract T CalculateChangeInValue(T initialValue, T targetValue);

        public void Reset(T newTarget)
        {
            InitialValue = _getValue();
            TargetValue = newTarget;
            ChangeInValue = CalculateChangeInValue(InitialValue, TargetValue);
        }

        public override sealed void Reset()
        {
            Reset(TargetValue);
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);

            var newValue = CalculateNewValue(CurrentValue);
            _setValue(newValue);
        }
    }

    public abstract class Transition
    {
        protected Transition(TransitionParameters transitionParameters)
        {
            Duration = transitionParameters.Duration;
            EasingFunction = transitionParameters.EasingFunction;
            Play();
        }

        public float CurrentTime { get; private set; }
        public float CurrentValue { get; private set; }
        public float Duration { get; private set; }
        public EasingFunction EasingFunction { get; set; }

        private bool _isComplete;
        public bool IsComplete
        {
            get { return _isComplete; }
            private set
            {
                if (_isComplete != value)
                {
                    _isComplete = value;
                    TransitionComplete.Raise(this, EventArgs.Empty);
                }
            }
        }

        public bool IsPaused { get; private set; }

        public event EventHandler TransitionComplete;

        public abstract void Reset();

        public void Pause()
        {
            IsPaused = true;
        }

        public void Resume()
        {
            IsPaused = false;
        }

        public void Play()
        {
            CurrentValue = 0.0f;
            IsComplete = false;
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
