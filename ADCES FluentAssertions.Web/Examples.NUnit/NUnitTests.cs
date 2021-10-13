using NUnit.Framework;
using System;
using System.Linq;

namespace Examples.NUnit
{
    public class NUnitTests
    {
        [Test]
        public void Booleans()
        {
            var result = false;

            Assert.IsTrue(result);
        }

        [Test]
        public void Booleans_ConstraintModel()
        {
            var result = false;

            Assert.That(result, Is.True);
        }

        [Test]
        public void Collections_Contains()
        {
            var numbers = new[] { 0, 2, 4, -1 };

            Assert.Contains(1, numbers);
        }

        [Test]
        public void Collections_Contains_ConstraintModel()
        {
            var numbers = new[] { 0, 2, 4, -1 };

            Assert.That(numbers, Contains.Item(1));

            //Assert.That(numbers, Does.Contain(1));
            //Assert.That(numbers, Does.Not.Contain(0));
            
            //Assert.That(numbers, Has.Member(1));
            //Assert.That(numbers, Has.No.Member(0));

            //Assert.That(numbers, Has.No.Member(0).And.No.Member(2));

        }

        [Test]
        public void BigCollections_Contains()
        {
            var numbers = Enumerable.Range(1, 100_000).ToList();

            Assert.Contains(-1, numbers);
        }

        [Test]
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

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Equality_ConstraintModel()
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

            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}
