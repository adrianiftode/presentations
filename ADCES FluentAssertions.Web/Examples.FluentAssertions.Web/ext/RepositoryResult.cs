using System;

namespace FluentAssertions
{
    public class RepositoryResult<TResult>
    {
        private RepositoryResult()
        {
        }

        public bool IsSuccess { get; private init; }

        public TResult Value { get; private init; }

        public string ErrorMessage { get; private init; }

        public static RepositoryResult<TResult> Fail(string errorMessage) => new RepositoryResult<TResult>()
        {
            IsSuccess = false,
            ErrorMessage = errorMessage
        };

        public static RepositoryResult<TResult> Fail() => new RepositoryResult<TResult>()
        {
            IsSuccess = false,
            ErrorMessage = "Unknown Error"
        };

        public static RepositoryResult<TResult> Success(TResult value) => (object) value != null
            ? new RepositoryResult<TResult>()
            {
                IsSuccess = true,
                Value = value
            }
            : throw new ArgumentNullException(nameof(value));

        public static implicit operator RepositoryResult<TResult>(TResult value) => RepositoryResult<TResult>.Success(value);
    }
}