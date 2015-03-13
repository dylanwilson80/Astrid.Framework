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
            var targetValue = 1.0f;
            var animation = new FloatAnimation(2.0f, 3.0f, a => targetValue = a, duration: 2.0f);

            animation.Update(0.0f);
            Assert.AreEqual(2.0f, targetValue);

            animation.Update(1.0f);
            Assert.AreEqual(2.5f, targetValue);

            animation.Update(2.0f);
            Assert.AreEqual(3.0f, targetValue);
        }

        [TestMethod]
        public void Animation_Update_IsCompleteAfterDuration_Test()
        {
            var targetValue = 1.0f;
            var animation = new FloatAnimation(2.0f, 3.0f, a => targetValue = a, duration: 2.0f);

            animation.Update(2.0f);
            Assert.IsTrue(animation.IsComplete);

            animation.Update(2.1f);
            Assert.AreEqual(3.0, targetValue);
        }

        [TestMethod]
        public void Animation_IsComplete_IfStopped_Test()
        {
            // ReSharper disable once NotAccessedVariable
            var targetValue = 1.0f;
            var animation = new FloatAnimation(2.0f, 3.0f, a => targetValue = a, duration: 2.0f);

            animation.Stop();
            Assert.IsTrue(animation.IsComplete);
        }

        [TestMethod]
        public void Animation_IsPaused_AfterPause_Test()
        {
            // ReSharper disable once NotAccessedVariable
            var targetValue = 1.0f;
            var animation = new FloatAnimation(2.0f, 3.0f, a => targetValue = a, duration: 2.0f);

            animation.Pause();
            Assert.IsTrue(animation.IsPaused);
        }
        
        [TestMethod]
        public void Animation_IsPaused_PreventsChange_Test()
        {
            // ReSharper disable once NotAccessedVariable
            var targetValue = 1.0f;
            var animation = new FloatAnimation(2.0f, 3.0f, a => targetValue = a, duration: 2.0f);

            animation.Update(1.0f);
            Assert.AreEqual(1.0f, animation.CurrentTime);

            animation.Pause();
            animation.Update(1.0f);
            Assert.AreEqual(1.0f, animation.CurrentTime);
            Assert.IsFalse(animation.IsComplete);
        }


        [TestMethod]
        public void Animation_Update_CustomEasingFunction_Test()
        {
            // ReSharper disable once NotAccessedVariable
            var targetValue = 1.0f;
            var animation = new FloatAnimation(2.0f, 3.0f, a => targetValue = a, duration: 2.0f)
            {
                EasingFunction = a => 1.2f
            };

            animation.Update(1.0f);
            Assert.AreEqual(3.2f, targetValue);
        }
    }
}
