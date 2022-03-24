using System;
using ApprovalTests;
using eBabyServices;
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
            var testSubject = Arbitrary.Auction();
            testSubject.OnStart();
            testSubject.Status.Should().Be(AuctionStatus.Running);
        }        
        
        [Fact]
        public void CanStopAuction()
        {
            var testSubject = Arbitrary.Auction();
            testSubject.OnStart();
            testSubject.OnClose();
            testSubject.Status.Should().Be(AuctionStatus.Closed);
        }
        
        [Fact]
        public void StopWithNoBidShouldNotifySeller()
        {
            var testSubject = Arbitrary.Auction();
            testSubject.OnStart();
            testSubject.OnClose();
            var postOffice = PostOffice.GetInstance();
            postOffice.DoesLogContain(testSubject.Seller.UserEmail, string.Empty).Should().BeTrue();
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