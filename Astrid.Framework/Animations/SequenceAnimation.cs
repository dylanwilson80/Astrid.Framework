using System.Collections.Generic;
using System.Linq;

namespace Astrid.Framework.Animations
{
    public class SequenceAnimation<T> : Animation<T>
    {
        internal SequenceAnimation(AnimationSystem animationSystem, T target)
            : base(animationSystem, target)
        {
            _queue = new Queue<Transition>();
        }

        private readonly Queue<Transition> _queue;

        public override void Attach(Transition transition)
        {
            _queue.Enqueue(transition);
        }

        public override void Play()
        {
            PlayNext();
        }

        private void PlayNext()
        {
            if (_queue.Any())
            {
                var transition = _queue.Dequeue();
                transition.Reset();
                transition.TransitionComplete += (sender, args) => PlayNext();
                AnimationSystem.Attach(transition);
            }
        }
    }
}