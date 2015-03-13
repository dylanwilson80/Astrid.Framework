using Astrid.Core;
using Astrid.Framework.Entities.Components;

namespace Astrid.Framework.Animations
{
    public class Actor<T>
    {
        internal Actor(AnimationSystem animationSystem, T target)
        {
            AnimationSystem = animationSystem;
            Target = target;
        }

        internal AnimationSystem AnimationSystem { get; private set; }
        internal T Target { get; private set; }
    }

    public static class FluentAnimationExtensions
    {
        public static Actor<T> MoveTo<T>(this Actor<T> actor, Vector2 position, float duration)
            where T : IMovable
        {
            var animation = new Vector2Animation(actor.Target.Position, position, 
                p => actor.Target.Position = p, duration);
            actor.AnimationSystem.Attach(animation);
            return actor;
        }

        public static Actor<T> RotateTo<T>(this Actor<T> actor, float rotation, float duration)
            where T : IRotatable
        {
            var animation = new FloatAnimation(actor.Target.Rotation, rotation, 
                r => actor.Target.Rotation = r, duration);
            actor.AnimationSystem.Attach(animation);
            return actor;
        }

        public static Actor<T> FadeIn<T>(this Actor<T> actor, float duration)
            where T : IColorable
        {
            return FadeTo(actor, 1.0f, duration);
        }

        public static Actor<T> FadeOut<T>(this Actor<T> actor, float duration)
            where T : IColorable
        {
            return FadeTo(actor, 0.0f, duration);
        }

        public static Actor<T> FadeTo<T>(this Actor<T> actor, float alpha, float duration)
            where T : IColorable
        {
            var animation = new ColorAnimation(actor.Target.Color, new Color(actor.Target.Color, alpha),
                c => actor.Target.Color = c, duration);
            actor.AnimationSystem.Attach(animation);
            return actor;
        }
    }
}