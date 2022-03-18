using System;
using FluentAssertions;
using Xunit;

namespace eBaby.Tests
{
    public class UserTest
    {
        [Fact]
        public void User_creation()
        {
            new User().Should().NotBeNull();
        }
    }
}
