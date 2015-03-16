namespace Astrid.Framework.Animations
{
    public class DelayTransition : Transition
    {
        public DelayTransition(float duration) 
            : base(new TransitionParameters(duration))
        {
        }

        public override void Reset()
        {
        }
    }
}