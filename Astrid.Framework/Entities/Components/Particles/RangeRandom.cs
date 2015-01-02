using System;
using Astrid.Core;

namespace Astrid.Framework.Entities.Components.Particles
{
    public class RangeRandom
    {
        public RangeRandom()
        {
            _random = new Random();
        }

        private readonly Random _random;

        public int GetInt(Range<int> range)
        {
            return _random.Next(range.Minimum, range.Maximum + 1);
        }

        public float GetFloat(Range<float> range)
        {
            // TODO: Not sure if this ever returns the maximum value
            var rangeSize = range.Maximum - range.Minimum;
            return range.Minimum + (float) _random.NextDouble() * rangeSize;
        }

        public float GetAngle()
        {
            var range = new Range<float>(MathHelper.Pi * -1f, MathHelper.Pi);
            return GetFloat(range);
        }

        public Vector2 GetUnitVector()
        {
            var angle = GetAngle();
            var x = (float) Math.Cos(angle);
            var y = (float) Math.Sin(angle);
            return new Vector2(x, y);
        }
    }
}