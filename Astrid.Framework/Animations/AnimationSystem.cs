using System.Collections.Generic;
using Astrid.Framework.Entities.Components;

namespace Astrid.Framework.Animations
{
    public class AnimationSystem
    {
        public AnimationSystem()
        {
            _animations = new List<Animation>(); 
        }

        private readonly List<Animation> _animations;

        public Actor CreateActor(ITransformable target)
        {
            return new Actor(this, target);
        }

        public void Update(float deltaTime)
        {
            // This is a for loop to allow for animations to be added during update
            // ReSharper disable once ForCanBeConvertedToForeach
            for (int i = 0; i < _animations.Count; i++)
            {
                var animation = _animations[i];
                animation.Update(deltaTime);
            }

            _animations.RemoveAll(i => i.IsComplete);
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