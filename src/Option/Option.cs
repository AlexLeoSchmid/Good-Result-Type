using System;

namespace Optional
{
    public static class Option
    {
        public static Option<T> Some<T>(T value) =>
            Option<T>.Some(value);

        public static Option<T> None<T>() =>
            Option<T>.None();
    }

    public struct Option<T>
    {
        private T _Value;

        public bool IsPresent { get; private set; }
        
        public static Option<T> Some(T value)
        {
            if(value == null)
            {
                throw new ArgumentNullException();
            }

            return new Option<T> { _Value = value, IsPresent = true };
        }

        public static Option<T> None()
        {
            return new Option<T> { _Value = default, IsPresent = false };
        }

        public T Unwrap()
        {
            if(IsPresent)
            {
                return _Value;
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public void Match(Action<T> some, Action none)
        {
            if(IsPresent)
            {
                some(_Value);
            }
            else
            {
                none();
            }
        }

        public TResult Match<TResult>(Func<T, TResult> some, Func<TResult> none)
        {
            if(IsPresent)
            {
                return some(_Value);
            }
            else
            {
                return none();
            }
        }
    }
}
