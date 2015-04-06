using System.Collections.Generic;

namespace Astrid.Animations
{
    public class ParallelAnimation<T> : Animation<T>
    {
        public ParallelAnimation(AnimationSystem animationSystem, T target) 
            : base(animationSystem, target)
        {
            _transitions = new List<Transition>();
        }

        private readonly List<Transition> _transitions; 

        public override void Attach(Transition transition)
        {
            _transitions.Add(transition);
        }

        public override void Play()
        {
            foreach (var transition in _transitions)
            {
                transition.Reset();
                AnimationSystem.Attach(transition);
            }
        }
    }
}