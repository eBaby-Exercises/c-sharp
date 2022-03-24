using System;
using ApprovalTests;
using FluentAssertions;
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
            sut.IsLoggedIn.Should().BeFalse();
            sut.IsSeller.Should().BeFalse();
        }

        [Fact]
        public void Auction_Properties()
        {
            var clock = new StoppedClock();
            Approvals.Verify(new Auction(Arbitrary.UserWithUserName("right_username"),
                "ItemDescr",23.95m,clock.Now(),clock.Now()));
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
            var user = Arbitrary.RegisteredUser(out var registry);
            User otherUser = Arbitrary.User();
            registry.Add(otherUser);
            var foundUser = registry.FindUser(user.UserName);
            foundUser.Should().BeSameAs(user);
            foundUser = registry.FindUser(otherUser.UserName);
            foundUser.Should().BeSameAs(otherUser);
        }

        [Fact]
        public void UserCanCreateAuction()
        {
            var user = Arbitrary.RegisteredUser(out var registry);
            user.BecomeSeller();
            var clock = new StoppedClock();
            var result = user.CreateAuction("ItemDescr", 23.95m, clock.Now()
                , clock.Now());
            result.Should().NotBeNull();
        }

        [Fact]
        public void User_must_be_a_seller_to_create_an_auction()
        {
            var user = Arbitrary.RegisteredUser(out var registry);
            Action makeSeller = () =>
            {
                var clock = new StoppedClock();
                user.CreateAuction("ItemDescr", 23.95m, clock.Now(), clock.Now());
            };
            makeSeller.Should().Throw<NotAuthorizedException>();
        }
        
        [Fact]
        public void User_Login_Success()
        {
            var user = Arbitrary.RegisteredUser(out var registry);
            user.IsLoggedIn.Should().BeFalse();
            registry.LogIn(user.UserName, "right_password");
            user.IsLoggedIn.Should().BeTrue();
        }        
        
        [Fact]
        public void User_Can_Become_Seller()
        {
            var user = Arbitrary.RegisteredUser(out var registry);
            user.IsSeller.Should().BeFalse();
            registry.MakeSeller(user.UserName);
            user.IsSeller.Should().BeTrue();
        } 
        
        [Fact]
        public void Making_seller_with_bad_username_fails()
        {
            var user = Arbitrary.RegisteredUser(out var registry);
            user.IsSeller.Should().BeFalse();
            Action makeSeller = () =>
            {
                registry.MakeSeller("incorrect_UserName");
            };
            makeSeller.Should().Throw<BadCredentialsException>();
            user.IsSeller.Should().BeFalse();
        }
        
        [Fact]
        public void User_Login_With_Bad_UserName()
        {
            var user = Arbitrary.RegisteredUser(out var registry);
            user.IsLoggedIn.Should().BeFalse();
            Action loginIn = () =>
            {
                registry.LogIn("incorrect_UserName", "right_password");
            };
            loginIn.Should().Throw<BadCredentialsException>();
            user.IsLoggedIn.Should().BeFalse();
        }

        [Fact]
        public void User_Login_Failure()
        {
            var user = Arbitrary.RegisteredUser(out var registry);
            user.IsLoggedIn.Should().BeFalse();
            Action loginIn = ()=>
            {
                registry.LogIn(user.UserName, "wrong_password");
            };
            loginIn.Should().Throw<BadCredentialsException>();
            user.IsLoggedIn.Should().BeFalse();
        }
        
        [Fact]
        public void User_LogOut()
        {
            var user = Arbitrary.User();
            user.TryToLogIn("right_password");
            user.LogOut();
            user.IsLoggedIn.Should().BeFalse();
        }
    }
}
