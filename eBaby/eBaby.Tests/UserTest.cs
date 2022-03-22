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

            sut.FirstName.Should().Be(firstName);
            sut.LastName.Should().Be(lastName);
            sut.UserEmail.Should().Be(userEmail);
            sut.UserName.Should().Be(userName);
            sut.Password.Should().Be(password);
        }

        [Fact]
        public void User_Registration()
        {
            User user = Arbitrary.User();
            UserRegistry registry = new UserRegistry();
            registry.Add(user);
            registry.RegisteredUsers.Should().BeEquivalentTo(new[] {user});
        }

        [Fact]
        public void RetrievingUserByUserName_Should_ReturnCorrectUser()
        {
            User user = Arbitrary.User();
            User otherUser = Arbitrary.User();
            UserRegistry registry = new UserRegistry();
            registry.Add(user);
            registry.Add(otherUser);
            var foundUser = registry.FindUser(user.UserName);
            foundUser.Should().BeSameAs(user);
            foundUser = registry.FindUser(otherUser.UserName);
            foundUser.Should().BeSameAs(otherUser);
        }

        [Fact]
        public void User_Login_Success()
        {

            User user = Arbitrary.User();
            UserRegistry registry = new UserRegistry();
            registry.Add(user);
            var foundUser = registry.FindUser(user.UserName);
            foundUser.Should().BeSameAs(user);
            foundUser.IsLoggedIn.Should().BeTrue();
            // If equal, then: set authenticated property to true
            // If not equal, then: throw BadCredentials exception
            // Verify that user logged in

        }
    }
}
