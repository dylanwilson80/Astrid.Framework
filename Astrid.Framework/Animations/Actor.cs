using Astrid.Core;
using Astrid.Framework.Entities.Components;

namespace Astrid.Framework.Animations
{
    public class Actor
    {
        private readonly AnimationSystem _animationSystem;
        private readonly ITransformable _target;

        internal Actor(AnimationSystem animationSystem, ITransformable target)
        {
            _animationSystem = animationSystem;
            _target = target;
        }

        public Actor MoveTo(Vector2 position, float duration, EasingFunction easingFunction)
        {
            var animation = new Vector2Animation(_target.Position, position, v => _target.Position = v, duration)
            {
                EasingFunction = easingFunction
            };
            _animationSystem.Attach(animation);
            return this;
        }

        public Actor RotateTo(float rotation, float duration, EasingFunction easingFunction)
        {
            var animation = new FloatAnimation(_target.Rotation, rotation, r => _target.Rotation = r, duration)
            {
                EasingFunction = easingFunction
            };
            _animationSystem.Attach(animation);
            return this;
        }

        public Actor ScaleTo(Vector2 scale, float duration, EasingFunction easingFunction)
        {
            var animation = new Vector2Animation(_target.Scale, scale, s => _target.Scale = s, duration)
            {
                EasingFunction = easingFunction
            };
            _animationSystem.Attach(animation);
            return this;
        }
    }
}