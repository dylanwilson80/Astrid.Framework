using System;

namespace Astrid.Framework.Entities.Components.Particles
{
    public class Range<T>
        where T : IComparable
    {
        public Range(T minimum, T maximum)
        {
            Minimum = minimum;
            Maximum = maximum;
        }

        public T Minimum { get; set; }
        public T Maximum { get; set; }

        public override string ToString()
        {
            return string.Format("{0} to {1}", Minimum, Maximum);
        }
    }
}