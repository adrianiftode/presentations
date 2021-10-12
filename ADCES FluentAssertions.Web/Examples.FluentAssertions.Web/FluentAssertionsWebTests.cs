using FluentAssertions;
using Xunit;

namespace Examples.FluentAssertions.Web
{
    public class FluentAssertionsWebTests
    {
        [Fact]
        public void SuccessResult_ShouldBeAsExpected()
        {
            var result = RepositoryResult<string>.Success("Adrian");

            result.Should().BeSuccess();
        }
    }
}
