using System;

namespace Result
{
    public struct Result<TValue, TError>
    {
        private TValue _Value;
        private TError _Error;

        public bool IsOk { get; private set; }
        public bool IsErr => !IsOk;

        public static Result<TValue, TError> Ok(TValue value)
        {
            if(value == null)
            {
                throw new InvalidOperationException();
            }

            return new Result<TValue, TError>
                { _Value = value, _Error = default, IsOk = true };
        }

        public static Result<TValue, TError> Err(TError err)
        {
            if(err == null)
            {
                throw new InvalidOperationException();
            }

            return new Result<TValue, TError>
                { _Value = default, _Error = err, IsOk = false };
        }

        public TValue Unwrap()
        {
            if(IsOk)
            {
                return _Value;
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public TError GetErr()
        {
            if(IsOk)
            {
                throw new InvalidOperationException();
            }
            else
            {
                return _Error;
            }
        }

        public void Match(Action<TValue> ok, Action<TError> err)
        {
            if(ok == null || err == null)
            {
                throw new ArgumentNullException();
            }
            if(IsOk)
            {
                ok(_Value);
            }
            else
            {
                err(_Error);
            }
        }

        public TResult Match<TResult>(Func<TValue, TResult> ok, Func<TError, TResult> err)
        {
            if(ok == null || err == null)
            {
                throw new ArgumentNullException();
            }
            if(IsOk)
            {
                return ok(_Value);
            }
            else
            {
                return err(_Error);
            }
        }
    }
}
