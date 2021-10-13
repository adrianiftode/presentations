using FluentAssertions.Formatting;
using System.Text;

namespace FluentAssertions
{
    internal class AssertionsFailuresFormatter : IValueFormatter
    {
        public bool CanHandle(object value) => value is AssertionsFailures;
        public void Format(object value,
            FormattedObjectGraph formattedGraph,
            FormattingContext context,
            FormatChild formatChild)
        {
            var assertionsFailures = (AssertionsFailures)value;

            var messageBuilder = new StringBuilder();
            messageBuilder.AppendLine();
            messageBuilder.AppendLine();

            foreach (var failure in assertionsFailures.FailuresMessages)
            {
                messageBuilder.AppendLine($"    - { ReplaceFirstWithLowercase(failure) }");
            }

            formattedGraph.AddFragment(messageBuilder.ToString());

            string ReplaceFirstWithLowercase(string source) => !string.IsNullOrEmpty(source) ?
                source[0].ToString().ToLower() + source.Substring(1)
                : source;
        }
    }
}