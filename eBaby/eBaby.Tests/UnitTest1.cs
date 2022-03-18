using System;
using FluentAssertions;
using Xunit;

namespace eBaby.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            new Class1().Should().NotBeNull();
        }
    }
}
