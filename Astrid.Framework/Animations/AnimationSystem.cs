using System.Collections.Generic;

namespace Astrid.Framework.Animations
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

        public void Attach(Transition animation)
        {
            _transitions.Add(animation);
        }

        public void Detach(Transition animation)
        {
            _transitions.Remove(animation);
        }

        //public Animation<T> CreateActor<T>(T target)
        //{
        //    return new Animation<T>(this, target);
        //}

        public SequenceAnimation<T> CreateSequence<T>(T target)
        {
            return new SequenceAnimation<T>(this, target);
        }
    }
}