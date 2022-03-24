using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            testSubject.Should().HaveHighBid(bidder, 30.0m);
        }
    }
}
