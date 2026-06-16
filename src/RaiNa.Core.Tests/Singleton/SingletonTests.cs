using NUnit.Framework;

namespace RaiNa.Tests.Singleton
{
    public class SingletonTests
    {
        private ConcreteSingleton singleton;

        [SetUp]
        public void Awake()
        {
            singleton = ConcreteSingleton.CreateInstance();
        }

        [Test]
        public void IsInstanceNull()
        {
            Assert.That(singleton, Is.Not.Null);
        }

        [Test]
        public void CallFuncTest()
        {
            bool val = singleton.Hello();
            Assert.That(val, Is.True);
        }

        [Test]
        public void IsEqualInstanceTest()
        {
            Assert.That(singleton, Is.SameAs(ConcreteSingleton.Instance));
        }

        [Test]
        public void CleanUpTest()
        {
            ConcreteSingleton.DestroyInstance();
            Assert.That(singleton, Is.Null);
        }
    }
}