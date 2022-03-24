using System;
using ApprovalTests;
using FluentAssertions;
using Xunit;

namespace eBaby.Tests
{
    public class StartStopAuction
    {
        [Fact]
        public void UserCanCreateAuction()
        {
            var user = Arbitrary.UserWithUserName("right_user");
            user.BecomeSeller();
            var clock = new StoppedClock();
            var result = user.CreateAuction("ItemDescr", 23.95m, clock.Now()
                , clock.Now());
            Approvals.Verify(result);
        }

        [Fact]
        public void CanStartAuction()
        {
            var result = Arbitrary.Auction();
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
    }
}