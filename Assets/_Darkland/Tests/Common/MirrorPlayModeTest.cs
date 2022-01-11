using System.Collections;
using Mirror.Tests;
using UnityEngine.TestTools;

namespace _Darkland.Tests.Common {

    public abstract class MirrorPlayModeTest : MirrorTest {
        // when overwriting, call it like this:
        //   yield return base.UnitySetUp();
        [UnitySetUp]
        public virtual IEnumerator UnitySetUp()
        {
            base.SetUp();
            yield return null;
        }

        // when overwriting, call it like this:
        //   yield return base.UnityTearDown();
        [UnityTearDown]
        public virtual IEnumerator UnityTearDown()
        {
            base.TearDown();
            yield return null;
        }
    }

}