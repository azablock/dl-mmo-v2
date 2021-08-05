using NUnit.Framework;

namespace _Darkland.Tests.Common {

    public abstract class MirrorEditModeTest : MirrorTest {
        [SetUp]
        public override void SetUp() => base.SetUp();

        [TearDown]
        public override void TearDown() => base.TearDown();
    }

}