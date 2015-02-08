using System;

namespace Astrid.Framework.Animations
{
    public delegate float EasingFunction(float time, float intialValue, float changeInValue, float duration);

    // http://gizma.com/easing/
    public static class EasingFunctions
    {
        public static float Linear(float t,  float b, float c, float d)
        {
	        return c * t / d + b;
        }

        public static float QuadraticEaseIn(float t, float b, float c, float d)
        {
            t /= d;
            return c * t * t + b;
        }

        public static float QuadraticEaseOut(float t, float b, float c, float d)
        {
            t /= d;
            return -c * t * (t - 2) + b;
        }

        public static float QuadraticEaseInOut(float t, float b, float c, float d)
        {
            t /= d / 2;
            if (t < 1) return c / 2 * t * t + b;
            t--;
            return -c / 2 * (t * (t - 2) - 1) + b;
        }

        public static float CubicEaseIn(float t, float b, float c, float d)
        {
            t /= d;
            return c * t * t * t + b;
        }

        public static float CubicEaseOut(float t, float b, float c, float d)
        {
            t /= d;
            t--;
            return c * (t * t * t + 1) + b;
        }

        public static float CubicEaseInOut(float t, float b, float c, float d)
        {
            t /= d / 2;
            if (t < 1) return c / 2 * t * t * t + b;
            t -= 2;
            return c / 2 * (t * t * t + 2) + b;
        }

        public static float QuarticEaseIn(float t, float b, float c, float d)
        {
            t /= d;
            return c * t * t * t * t + b;
        }

        public static float QuarticEaseOut(float t, float b, float c, float d)
        {
            t /= d;
            t--;
            return -c * (t * t * t * t - 1) + b;
        }

        public static float QuarticEaseInOut(float t, float b, float c, float d)
        {
            t /= d / 2;
            if (t < 1) return c / 2 * t * t * t * t + b;
            t -= 2;
            return -c / 2 * (t * t * t * t - 2) + b;
        }

        public static float QuinticEaseIn(float t, float b, float c, float d)
        {
            t /= d;
            return c * t * t * t * t * t + b;
        }

        public static float QuinticEaseOut(float t, float b, float c, float d)
        {
            t /= d;
            t--;
            return c * (t * t * t * t * t + 1) + b;
        }

        public static float QuinticEaseInOut(float t, float b, float c, float d)
        {
            t /= d / 2;
            if (t < 1) return c / 2 * t * t * t * t * t + b;
            t -= 2;
            return c / 2 * (t * t * t * t * t + 2) + b;
        }

        public static float SineEaseIn(float t, float b, float c, float d)
        {
            return (float) (-c * Math.Cos(t / d * (Math.PI / 2)) + c + b);
        }

        public static float SineEaseOut(float t, float b, float c, float d)
        {
            return (float) (c * Math.Sin(t / d * (Math.PI / 2)) + b);
        }

        public static float SineEaseInOut(float t, float b, float c, float d)
        {
            return (float) (-c / 2 * (Math.Cos(Math.PI * t / d) - 1) + b);
        }

    }
}