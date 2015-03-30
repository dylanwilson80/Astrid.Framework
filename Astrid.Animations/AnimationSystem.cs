using System.Collections.Generic;

namespace Astrid.Animations
{
    public class AnimationSystem
    {
        public AnimationSystem()
        {
            _transitions = new List<Transition>(); 
        }

        private readonly List<Transition> _transitions;

        public void Update(float deltaTime)
        {
            // This is a for loop to allow for animations to be added during update
            // ReSharper disable once ForCanBeConvertedToForeach
            for (int i = 0; i < _transitions.Count; i++)
            {
                var transition = _transitions[i];
                transition.Update(deltaTime);
            }

            _transitions.RemoveAll(i => i.IsComplete);
        }

        public void Attach(Transition transition)
        {
            _transitions.Add(transition);
        }

        public void Detach(Transition transition)
        {
            _transitions.Remove(transition);
        }

        public ParallelAnimation<T> CreateParallel<T>(T target)
        {
            return new ParallelAnimation<T>(this, target);
        }

        public SequenceAnimation<T> CreateSequence<T>(T target)
        {
            return new SequenceAnimation<T>(this, target);
        }
    }
}