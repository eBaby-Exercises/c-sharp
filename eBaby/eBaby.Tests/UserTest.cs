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
            string firstName = "James";
            string lastName = "Bond";
            string userEmail = "somebody@example.com";
            string userName = "[username]";
            string password = "[pWord]";
            var sut = new User(firstName, lastName, userEmail, userName, password);
            sut.firstName.Should().Be("James");
            sut.lastName.Should().Be("Bond");
            sut.userEmail.Should().Be("somebody@example.com");
            sut.userName.Should().Be("[username]");
            sut.password.Should().Be("[pWord]");
        }
    }
}
