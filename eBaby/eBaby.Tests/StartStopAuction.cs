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
            var postOffice = PostOffice.GetNewInstance();
            var testSubject = Arbitrary.Auction(postOffice);
            testSubject.OnStart();
            testSubject.OnClose();
            postOffice.Should().HaveSeenMsg(testSubject.Seller.UserEmail,
                EmailMessages.AuctionClosedWithoutBids(testSubject.Itemdescr));
        }
        
        [Fact]
        public void StopWithBiddersShouldNotifySellerAndBuyer()
        {
            var buyer = Arbitrary.User();
            var postOffice = PostOffice.GetNewInstance();
            var testSubject = Arbitrary.Auction(postOffice);

            testSubject.OnStart();
            buyer.Bid(testSubject, 23.00M);
            testSubject.OnClose();

            postOffice.Should().HaveSeenMsg(testSubject.Seller.UserEmail,
                EmailMessages.AuctionClosedWithBid(testSubject.Itemdescr));
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