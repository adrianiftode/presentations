using FluentAssertions.Execution;

namespace FluentAssertions
{
    internal static class FluentAssertionsHelpers
    {
        public static void ExecuteNotNull(object subject, string because, object[] becauseArgs)
        {
            Execute.Assertion
                .ForCondition(!ReferenceEquals(subject, null))
                .BecauseOf(because, becauseArgs)
                .FailWith("Expected {context} to assert {reason}, but found <null>.");
        }

        public static void ExecuteErrorMessageAssertion(string errorMessage, string expectedErrorMessage, object subject,
            string because, object[] becauseArgs)
        {
            Execute.Assertion
                .BecauseOf(because, becauseArgs)
                .ForCondition(!string.IsNullOrEmpty(errorMessage))
                .FailWith("Expected {context} to have an error message{reason}, but there is no error message set.{0}", subject);

            string[] failures;
            using (var scope = new AssertionScope())
            {
                errorMessage.Should().Match(expectedErrorMessage);
                failures = scope.Discard();
            }

            Execute.Assertion
                .BecauseOf(because, becauseArgs)
                .ForCondition(failures.Length == 0)
                .FailWith("Expected ErrorMessage to match `{1}`{reason}, but it doesn't.{0}", subject, expectedErrorMessage);
        }
    }
}