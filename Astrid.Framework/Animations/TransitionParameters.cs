namespace Astrid.Framework.Animations
{
    public class TransitionParameters
    {
        public TransitionParameters(float duration)
            : this(duration, EasingFunctions.Linear)
        {
        }

        public TransitionParameters(float duration, EasingFunction easingFunction)
        {
            Duration = duration;
            EasingFunction = easingFunction;
        }

        public float Duration { get; set; }
        public EasingFunction EasingFunction { get; set; }
    }
}