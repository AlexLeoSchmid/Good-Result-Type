using System;

namespace Result
{
    public struct Result<TError>
    {
        private TError _Error;

        public bool IsOk { get; private set; }
        public bool IsErr => !IsOk;

        public static Result<TError> Ok()
        {
            return new Result<TError>
                { _Error = default, IsOk = true };
        }

        public static Result<TError> Err(TError err)
        {
            if(err == null)
            {
                throw new ArgumentNullException();
            }

            return new Result<TError>
                { _Error = err, IsOk = false };
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

        public void Match(Action ok, Action<TError> err)
        {
            if(ok == null || err == null)
            {
                throw new ArgumentNullException();
            }
            if(IsOk)
            {
                ok();
            }
            else
            {
                err(_Error);
            }
        }

        public TResult Match<TResult>(Func<TResult> ok, Func<TError, TResult> err)
        {
            if(ok == null || err == null)
            {
                throw new ArgumentNullException();
            }
            if(IsOk)
            {
                return ok();
            }
            else
            {
                return err(_Error);
            }
        }
    }
}