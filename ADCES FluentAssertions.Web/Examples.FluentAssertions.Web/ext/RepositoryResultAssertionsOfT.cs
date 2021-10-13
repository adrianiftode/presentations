using FluentAssertions.Execution;
using FluentAssertions.Formatting;
using FluentAssertions.Primitives;

namespace FluentAssertions
{
    public class RepositoryResultAssertions<T> : ReferenceTypeAssertions<RepositoryResult<T>, RepositoryResultAssertions<T>>
    {
        static RepositoryResultAssertions() => Formatter.AddFormatter(new AssertionsFailuresFormatter());

        public RepositoryResultAssertions(RepositoryResult<T> value) : base(value)
        {
        }

        protected override string Identifier => "result_t";

        [CustomAssertion]
        public AndConstraint<SuccessfulRepositoryResultAssertions<T>> BeSuccess(string because = "", params object[] becauseArgs)
        {
            FluentAssertionsHelpers.ExecuteNotNull(Subject, because, becauseArgs);

            Execute.Assertion
                .BecauseOf(because, becauseArgs)
                .ForCondition(Subject.IsSuccess)
                .FailWith("Expected {context} be a successful result{reason}.{0}", Subject);

            return new AndConstraint<SuccessfulRepositoryResultAssertions<T>>(new SuccessfulRepositoryResultAssertions<T>(Subject));
        }

        [CustomAssertion]
        public AndConstraint<FailedRepositoryResultAssertions<T>> BeFail(string because = "", params object[] becauseArgs)
        {
            FluentAssertionsHelpers.ExecuteNotNull(Subject, because, becauseArgs);

            Execute.Assertion
                .BecauseOf(because, becauseArgs)
                .ForCondition(!Subject.IsSuccess)
                .FailWith("Expected {context} to be an unsuccessful result{reason}.{0}", Subject);

            return new AndConstraint<FailedRepositoryResultAssertions<T>>(new FailedRepositoryResultAssertions<T>(Subject));
        }
    }
}