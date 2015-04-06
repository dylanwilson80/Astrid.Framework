using System;
using Astrid.Core;

namespace Astrid.Animations
{
    public static class AnimationExtensions
    {
        public static Animation<T> MoveTo<T>(this Animation<T> animation, Vector2 position, TransitionParameters transitionParameters)
            where T : IMovable
        {
            animation.Attach(new Vector2Transition(() => animation.Target.Position, p => animation.Target.Position = p, position, transitionParameters));
            return animation;
        }

        public static Animation<T> ScaleTo<T>(this Animation<T> animation, Vector2 scale, TransitionParameters transitionParameters)
            where T : IScalable
        {
            animation.Attach(new Vector2Transition(() => animation.Target.Scale, s => animation.Target.Scale = s, scale, transitionParameters));
            return animation;
        }

        public static Animation<T> RotateTo<T>(this Animation<T> animation, float rotation, TransitionParameters transitionParameters)
            where T : IRotatable
        {
            animation.Attach(new FloatTransition(() => animation.Target.Rotation, r => animation.Target.Rotation = r, rotation, transitionParameters));
            return animation;
        }

        public static Animation<T> FadeIn<T>(this Animation<T> animation, TransitionParameters transitionParameters)
            where T : IColorable
        {
            return FadeTo(animation, 1.0f, transitionParameters);
        }

        public static Animation<T> FadeOut<T>(this Animation<T> animation, TransitionParameters transitionParameters)
            where T : IColorable
        {
            return FadeTo(animation, 0.0f, transitionParameters);
        }

        public static Animation<T> FadeTo<T>(this Animation<T> animation, float alpha, TransitionParameters transitionParameters)
            where T : IColorable
        {
            animation.Attach(new ColorTransition(() => animation.Target.Color, c => animation.Target.Color = c, new Color(animation.Target.Color, alpha), transitionParameters));
            return animation;
        }

        public static Animation<T> Delay<T>(this Animation<T> animation, float duration)
        {
            animation.Attach(new DelayTransition(duration));
            return animation;
        }

        public static Animation<T> Execute<T>(this Animation<T> animation, Action action)
        {
            animation.Attach(new ExecuteTransition(action));
            return animation;
        }
    }
}