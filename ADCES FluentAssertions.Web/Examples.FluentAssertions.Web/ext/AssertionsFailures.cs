using System.Collections.Generic;

namespace FluentAssertions
{
    internal class AssertionsFailures
    {
        public AssertionsFailures(string[] failuresMessages)
        {
            FailuresMessages = failuresMessages;
        }
        public IReadOnlyCollection<string> FailuresMessages { get; }
    }
}