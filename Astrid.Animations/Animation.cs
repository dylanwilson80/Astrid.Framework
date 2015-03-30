namespace Astrid.Animations
{
    public abstract class Animation<T>
    {
        internal Animation(AnimationSystem animationSystem, T target)
        {
            AnimationSystem = animationSystem;
            Target = target;
        }

        protected AnimationSystem AnimationSystem { get; private set; }
        internal T Target { get; private set; }

        public abstract void Attach(Transition transition);
        public abstract void Play();
    }
}