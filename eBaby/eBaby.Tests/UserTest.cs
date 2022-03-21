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
            string lastName = string.Empty;
            string userEmail = string.Empty;
            string userName = string.Empty;
            string password = string.Empty;
            var sut = new User(firstName, lastName, userEmail, userName, password);

            Assert.Equal("James", sut.firstName);


        }
    }
}
