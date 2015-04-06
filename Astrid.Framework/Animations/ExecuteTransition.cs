using System;

namespace Astrid.Animations
{
    public class ExecuteTransition : Transition
    {
        public ExecuteTransition(Action action)
            : base(new TransitionParameters(0.0f))
        {
            _action = action;
        }

        private readonly Action _action;

        public override void Reset()
        {
            _action();
        }
    }
}