using System;
using NUnit.Framework;
using Result;

namespace ResultTest
{
    public class ResultWithValueTest
    {

        [Test]
        public void OkTest()
        {
            var OkString = Result<string, string>.Ok("abc");
            Assert.That(OkString.IsOk);
            Assert.IsFalse(OkString.IsErr);
            Assert.DoesNotThrow(() => OkString.Unwrap());
            Assert.Throws<InvalidOperationException>(() => OkString.GetErr());
        }

        [Test]
        public void OkMatchTest()
        {
            var OkString = Result<string, string>.Ok("abc");
            OkString.Match(okValue => Assert.Pass(), errValue => Assert.Fail());
        }

        [Test]
        public void OkMatchWithResultTest()
        {
            var OkString = Result<string, string>.Ok("abc");
            bool IsOk = OkString.Match(okValue => true, errValue => false);
            Assert.That(IsOk);
        }

        [Test]
        public void ErrMatchTest()
        {
            var ErrString = Result<string, string>.Err("abc");
            ErrString.Match(okValue => Assert.Fail(), errValue => Assert.Pass());
        }

        [Test]
        public void ErrMatchWithResultTest()
        {
            var ErrString = Result<string, string>.Err("abc");
            bool IsErr = ErrString.Match(okValue => false, errValue => true);
            Assert.That(IsErr);
        }

        [Test]
        public void ErrTest()
        {
            var ErrString = Result<string, string>.Err("abc");
            Assert.IsFalse(ErrString.IsOk);
            Assert.That(ErrString.IsErr);
            Assert.Throws<InvalidOperationException>(() => ErrString.Unwrap());
            Assert.DoesNotThrow(() => ErrString.GetErr());
        }

        [Test]
        public void OkNullTest()
        {
            Assert.Throws<InvalidOperationException>(() => Result<string, string>.Ok(null));
        }

        [Test]
        public void ErrNullTest()
        {
            Assert.Throws<InvalidOperationException>(() => Result<string, string>.Err(null));
        }

        [Test]
        public void MatchNullDelegateTest()
        {
            var OkString = Result<string, string>.Ok("abc");
            Assert.Throws<ArgumentNullException>(() => OkString.Match(null, null));
        }

        [Test]
        public void MatchWithResultNullDelegateTest()
        {
            var OkString = Result<string, string>.Ok("abc");
            Func<string, string> okDelegate = null;
            Func<string, string> errDelegate = null;
            Assert.Throws<ArgumentNullException>(() => OkString.Match(okDelegate, errDelegate));
        }
    }
}