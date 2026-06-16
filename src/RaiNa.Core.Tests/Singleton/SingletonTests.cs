using NUnit.Framework;
using RaiNa.Exceptions;
using System;

namespace RaiNa.Tests.Singleton
{
    public class SingletonTests
    {
        private ConcreteSingleton singleton;

        [SetUp]
        public void Awake()
        {
            singleton = ConcreteSingleton.Instance;
        }

        [Test]
        public void IsInvalidInitTest()
        {
            Assert.Catch(typeof(SingletonInitializeException), () =>
            {
                ConcreteSingleton instance = new();
            });
        }

        [Test]
        public void IsInstanceNotNullTest()
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
            Assert.That(ConcreteSingleton.IsInstanceExists, Is.False);
        }
    }
}