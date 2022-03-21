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
            sut.FirstName.Should().Be("James");
            sut.LastName.Should().Be("Bond");
            sut.UserEmail.Should().Be("somebody@example.com");
            sut.UserName.Should().Be("[username]");
            sut.Password.Should().Be("[pWord]");
        }
    }
}
