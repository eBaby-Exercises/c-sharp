using FluentAssertions;
using FluentAssertions.Primitives;

namespace eBaby.Tests
{
    internal class AuctionAssertions : ReferenceTypeAssertions<Auction, AuctionAssertions>
    {
        public AuctionAssertions(Auction subject) : base(subject)
        {
        }

        protected override string Identifier => "Auction";

        public void HaveHighBid(User bidder, decimal bid)
        {
            Subject.HighestBidder.Should().Be(bidder);
            Subject.HighestBid.Should().Be(bid);
        }
    }
}