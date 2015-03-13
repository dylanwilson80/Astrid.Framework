using Astrid.Core;
using Astrid.Framework.Entities.Components;

namespace Astrid.Framework.Animations
{
    public static class AnimationExtensions
    {
        public static Animation<T> MoveTo<T>(this Animation<T> animation, Vector2 position, float duration)
            where T : IMovable
        {
            var transition = new Vector2Transition(animation.Target.Position, position, 
                p => animation.Target.Position = p, duration);
            animation.Attach(transition);
            return animation;
        }

        public static Animation<T> RotateTo<T>(this Animation<T> animation, float rotation, float duration)
            where T : IRotatable
        {
            var transition = new FloatTransition(animation.Target.Rotation, rotation, 
                r => animation.Target.Rotation = r, duration);
            animation.Attach(transition);
            return animation;
        }

        public static Animation<T> FadeIn<T>(this Animation<T> animation, float duration)
            where T : IColorable
        {
            return FadeTo(animation, 1.0f, duration);
        }

        public static Animation<T> FadeOut<T>(this Animation<T> animation, float duration)
            where T : IColorable
        {
            return FadeTo(animation, 0.0f, duration);
        }

        public static Animation<T> FadeTo<T>(this Animation<T> animation, float alpha, float duration)
            where T : IColorable
        {
            var transition = new ColorTransition(animation.Target.Color, new Color(animation.Target.Color, alpha),
                c => animation.Target.Color = c, duration);
            animation.Attach(transition);
            return animation;
        }
    }
}