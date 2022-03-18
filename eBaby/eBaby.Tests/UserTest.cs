using System;
using FluentAssertions;
using Microsoft.VisualBasic;
using Xunit;

namespace eBaby.Tests
{
    public class UserTest
    {
        [Fact]
        public void User_Creation()
        {
            string firstName = string.Empty;
            string lastName = string.Empty;
            string userEmail = string.Empty;
            string userName = string.Empty;
            string password = string.Empty;
            new User(firstName, lastName, userEmail, userName, password).Should().NotBeNull();
        }
    }
}
