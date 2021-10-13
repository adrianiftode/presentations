using FluentAssertions.Execution;
using System;
using System.Linq;

namespace FluentAssertions
{
    public class SuccessfulRepositoryResultAssertions<T> : RepositoryResultAssertions<T>
    {
        public SuccessfulRepositoryResultAssertions(RepositoryResult<T> value) : base(value) { }

        public T Value => Subject.Value;

        [CustomAssertion]
        public AndConstraint<SuccessfulRepositoryResultAssertions<T>> HaveValueAs(object expectedValue, string because = "", params object[] becauseArgs)
        {
            FluentAssertionsHelpers.ExecuteNotNull(Subject, because, becauseArgs);

            string[] failures;
            using (var scope = new AssertionScope())
            {
                Subject.Value.Should().BeEquivalentTo(expectedValue);

                failures = scope.Discard();
            }

            Execute.Assertion
                .BecauseOf(because, becauseArgs)
                .ForCondition(failures.Length == 0)
                .FailWith("Expected {context} to have a value equivalent to an expected one, but is has differences:{0} {reason}.{1}",
                    new AssertionsFailures(failures),
                    Subject);
            return new AndConstraint<SuccessfulRepositoryResultAssertions<T>>(this);
        }

        [CustomAssertion]
        public AndConstraint<RepositoryResultAssertions<T>> HaveValue(T expectedValue, string because = "", params object[] becauseArgs)
        {
            FluentAssertionsHelpers.ExecuteNotNull(Subject, because, becauseArgs);

            string[] failures;

            using (var scope = new AssertionScope())
            {
                Subject.Value.Should().BeEquivalentTo(expectedValue);

                failures = scope.Discard();
            }

            Execute.Assertion
                .BecauseOf(because, becauseArgs)
                .ForCondition(failures.Length == 0)
                .FailWith("Expected {context} to have a value equal to an expected one, but is has differences:{0} {reason}.{1}",
                    new AssertionsFailures(failures),
                    Subject);

            return new AndConstraint<RepositoryResultAssertions<T>>(this);
        }

        [CustomAssertion]
        public AndConstraint<RepositoryResultAssertions<T>> Satisfy(Action<T> assertion, string because = "", params object[] becauseArgs)
        {
            if (assertion == null)
            {
                throw new ArgumentException("Expected error message cannot be null.", nameof(assertion));
            }

            FluentAssertionsHelpers.ExecuteNotNull(Subject, because, becauseArgs);

            var failuresFromAssertions = CollectFailuresFromAssertion(assertion, Subject.Value);

            if (failuresFromAssertions.Any())
            {
                Execute.Assertion
                    .BecauseOf(because, becauseArgs)
                    .FailWith(
                        "Expected {context} to satisfy one or more model assertions, but it wasn't{reason}: {0}{1}",
                        new AssertionsFailures(failuresFromAssertions), Subject);
            }

            return new AndConstraint<RepositoryResultAssertions<T>>(this);
        }

        private string[] CollectFailuresFromAssertion<TAsserted>(Action<TAsserted> assertion, TAsserted subject)
        {
            using var collectionScope = new AssertionScope();
            string[] assertionFailures;
            using (var itemScope = new AssertionScope())
            {
                try
                {
                    assertion(subject);
                    assertionFailures = itemScope.Discard();
                }
                catch (Exception ex)
                {
                    assertionFailures = new[] { $"Expected to successfully verify an assertion, but the following exception occurred: { ex }" };
                }

            }

            foreach (var assertionFailure in assertionFailures)
            {
                collectionScope.AddPreFormattedFailure($"{assertionFailure}");
            }

            return collectionScope.Discard();
        }
    }
}