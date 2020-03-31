using System;
using NUnit.Framework;
using Optional;

namespace OptionalTest
{
    public class OptionTests
    {
        [Test]
        public void SomeTest()
        {
            var SomeString = Option.Some("abc");
            Assert.That(SomeString.IsPresent);
            Assert.DoesNotThrow(() => SomeString.Unwrap());
        }

        [Test]
        public void SomeMatchTest()
        {
            var SomeString = Option.Some("abc");
            SomeString.Match(stringValue => Assert.Pass(), () => Assert.Fail());
        }

        [Test]
        public void SomeCMatchWithResultTest()
        {
            var SomeString = Option.Some("abc");
            bool IsPresent = SomeString.Match(stringValue => true, () => false);
            Assert.That(IsPresent);
        }

        [Test]
        public void NoneMatchTest()
        {
            var SomeString = Option.None<string>();
            SomeString.Match(stringValue => Assert.Fail(), () => Assert.Pass());
        }

        [Test]
        public void NoneMatchWithResultTest()
        {
            var SomeString = Option.None<string>();
            bool IsNone = SomeString.Match(stringValue => false, () => true);
            Assert.That(IsNone);
        }

        [Test]
        public void NoneTest()
        {
            var NoNumber = Option.None<string>();
            Assert.IsFalse(NoNumber.IsPresent);
            Assert.Throws<InvalidOperationException>(() => NoNumber.Unwrap());
        }

        [Test]
        public void SomeNullTest()
        {
            Assert.Throws<ArgumentNullException>(() => Option.Some<string>(null));
        }
    }
}