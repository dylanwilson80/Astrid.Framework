using System.Collections.Generic;

namespace Astrid.Framework.Animations
{
    public class AnimationSystem
    {
        public AnimationSystem()
        {
            _animations = new List<Animation>(); 
        }

        private readonly List<Animation> _animations;

        public void Update(float deltaTime)
        {
            foreach (var animation in _animations)
                animation.Update(deltaTime);

            _animations.RemoveAll(i => !i.IsPlaying);
        }

        public void Attach(Animation animation)
        {
            _animations.Add(animation);
        }

        public void Detach(Animation animation)
        {
            _animations.Remove(animation);
        }

        public T CreateAnimation<T>(float duration)
            where T : Animation, new()
        {
            var animation = new T();
            _animations.Add(animation);
            return animation;
        }
    }
}