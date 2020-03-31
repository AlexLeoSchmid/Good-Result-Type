using System;
using NUnit.Framework;
using Result;

namespace ResultTest
{
    public class ResultWithoutValueTest
    {

        [Test]
        public void OkTest()
        {
            var OkString = Result<string>.Ok();
            Assert.That(OkString.IsOk);
            Assert.IsFalse(OkString.IsErr);
            Assert.Throws<InvalidOperationException>(() => OkString.GetErr());
        }

        [Test]
        public void OkMatchTest()
        {
            var OkString = Result<string>.Ok();
            OkString.Match(() => Assert.Pass(), errValue => Assert.Fail());
        }

        [Test]
        public void OkMatchWithResultTest()
        {
            var OkString = Result<string>.Ok();
            bool IsOk = OkString.Match(() => true, errValue => false);
            Assert.That(IsOk);
        }

        [Test]
        public void ErrMatchTest()
        {
            var ErrString = Result<string>.Err("abc");
            ErrString.Match(() => Assert.Fail(), errValue => Assert.Pass());
        }

        [Test]
        public void ErrMatchWithResultTest()
        {
            var ErrString = Result<string>.Err("abc");
            bool IsErr = ErrString.Match(() => false, errValue => true);
            Assert.That(IsErr);
        }

        [Test]
        public void ErrTest()
        {
            var ErrString = Result<string>.Err("abc");
            Assert.IsFalse(ErrString.IsOk);
            Assert.That(ErrString.IsErr);
            Assert.DoesNotThrow(() => ErrString.GetErr());
        }

        [Test]
        public void ErrNullTest()
        {
            Assert.Throws<ArgumentNullException>(() => Result<string>.Err(null));
        }

        [Test]
        public void MatchNullDelegateTest()
        {
            var OkString = Result<string>.Ok();
            Assert.Throws<ArgumentNullException>(() => OkString.Match(null, null));
        }

        [Test]
        public void MatchWithResultNullDelegateTest()
        {
            var OkString = Result<string>.Ok();
            Func<string> okDelegate = null;
            Func<string, string> errDelegate = null;
            Assert.Throws<ArgumentNullException>(() => OkString.Match(okDelegate, errDelegate));
        }
    }
}