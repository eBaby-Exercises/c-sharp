using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using FluentAssertions.Primitives;
using Xunit;

namespace eBaby.Tests
{
    public class AuctionBidding
    {
        [Fact]
        public void BidOnRunningAuction()
        {
            var testSubject = Arbitrary.Auction();
            var bidder = Arbitrary.User();
            testSubject.OnStart();
            bidder.Bid(testSubject, 30.0m);
            Shouldd(testSubject).BeWonBy(bidder, 30.0m);
        }

        private AuctionAssertions Shouldd(Auction testSubject)
        {
            return new AuctionAssertions(testSubject);
        }
    }

    internal class AuctionAssertions : ReferenceTypeAssertions<Auction, AuctionAssertions>
    {
        public AuctionAssertions(Auction subject) : base(subject)
        {
        }

        protected override string Identifier => "Auction";

        public void BeWonBy(User bidder, decimal bid)
        {
            Subject.HighestBid.Should().Be(bid);
        }
    }
}
