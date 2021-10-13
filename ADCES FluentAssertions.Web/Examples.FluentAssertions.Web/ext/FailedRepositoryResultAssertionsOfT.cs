using System;
using System.Diagnostics;

namespace FluentAssertions
{
    [DebuggerNonUserCode]
    public static class RepositoryResultFluentAssertionsExtensions
    {
        public static RepositoryResultAssertions<TResult> Should<TResult>(this RepositoryResult<TResult> actual)
            => new RepositoryResultAssertions<TResult>(actual);
    }

    public class FailedRepositoryResultAssertions<T> : RepositoryResultAssertions<T>
    {
        public FailedRepositoryResultAssertions(RepositoryResult<T> value) : base(value)
        {
        }

        public T Value => Subject.Value;

        [CustomAssertion]
        public AndConstraint<FailedRepositoryResultAssertions<T>> HaveErrorMessage(string expectedErrorMessage, string because = "", params object[] becauseArgs)
        {
            if (string.IsNullOrEmpty(expectedErrorMessage))
            {
                throw new ArgumentException("Expected error message cannot be null.", nameof(expectedErrorMessage));
            }

            FluentAssertionsHelpers.ExecuteNotNull(Subject, because, becauseArgs);

            var errorMessage = Subject.ErrorMessage;

            FluentAssertionsHelpers.ExecuteErrorMessageAssertion(errorMessage, expectedErrorMessage, Subject, because, becauseArgs);

            return new AndConstraint<FailedRepositoryResultAssertions<T>>(this);
        }
    }
}