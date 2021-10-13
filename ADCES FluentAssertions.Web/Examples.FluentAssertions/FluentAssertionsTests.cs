using FluentAssertions;
using System;
using System.Linq;
using Xunit;

namespace Examples.FluentAssertions
{
    public class FluentAssertionsTests
    {
        [Fact]
        public void Booleans()
        {
            var result = false;

            result.Should().BeTrue();
        }

        [Fact]
        public void Booleans_WeExpected()
        {
            var result = false;

            result.Should().BeTrue(" today is {0:dddd} evening" , DateTime.Today);
        }

        [Fact]
        public void Collections_Contains()
        {
            var numbers = new[] { 0, 2, 4, -1 };

            numbers.Should().Contain(1);

            //using var _ = new AssertionScope();
            //numbers.Should().NotContain(0).And.NotContain(2);
        }

        [Fact]
        public void BigCollections_Contains()
        {
            var numbers = Enumerable.Range(1, 100_000).ToList();

            numbers.Should().Contain(-1);
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

            actual.Should().Be(expected);
        }

        [Fact]
        public void Equality_WithOptions()
        {
            var actual = new
            {
                Id = new Guid("ba4e3dc2-a98f-4a8c-a526-b38614d776cf"),
                Name = "My Company",
                Address = "Street No 1"
            };
            var expected = new
            {
                Id = new Guid("aa4e3dc2-a98f-4a8c-a526-b38614d776cf"),
                Name = "My Company"
            };

            actual.Should().BeEquivalentTo(expected, config => config.Excluding(exp => exp.Id));
        }
    }
}
