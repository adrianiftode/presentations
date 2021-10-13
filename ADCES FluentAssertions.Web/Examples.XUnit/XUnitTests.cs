using System;
using System.Linq;
using Xunit;

namespace Examples.XUnit
{
    public class XUnitTests
    {
        [Fact]
        public void Booleans()
        {
            var result = false;

            Assert.True(result);
        }

        [Fact]
        public void Collections_Contains()
        {
            var numbers = new[] { 0, 2, 4, -1 };

            Assert.Contains(1, numbers);
        }

        [Fact]
        public void BigCollections_Contains()
        {
            var numbers = Enumerable.Range(1, 100_000);

            Assert.Contains(-1, numbers);
        }

        [Fact]
        public void Equality_Value()
        {
            var actual = new
            {
                Id = new Guid("ba4e3dc2-a98f-4a8c-a526-b38614d776cf"),
                Name = "My Company"
            };
            var expected = new
            {
                Id = new Guid("aa4e3dc2-a98f-4a8c-a526-b38614d776cf"),
                Name = "My Company"
            };

            Assert.Equal(expected, actual);
        }
    }
}
