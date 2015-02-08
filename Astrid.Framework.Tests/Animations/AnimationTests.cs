using Astrid.Framework.Animations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Astrid.Framework.Tests.Animations
{
    [TestClass]
    public class AnimationTests
    {
        [TestMethod]
        public void Animation_Update_Test()
        {
            var animation = new Animation(duration: 2.0f);

            Assert.AreEqual(0.0f, animation.CurrentValue);

            animation.Update(1.0f);
            Assert.AreEqual(0.5f, animation.CurrentValue);

            animation.Update(2.0f);
            Assert.AreEqual(1.0f, animation.CurrentValue);
        }

        [TestMethod]
        public void Animation_Update_BoundedLessThanOrEqualToOne_Test()
        {
            var animation = new Animation(duration: 2.0f);

            animation.Update(2.1f);
            Assert.AreEqual(1.0f, animation.CurrentValue);
        }
    }
}
