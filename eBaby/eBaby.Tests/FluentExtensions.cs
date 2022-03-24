namespace eBaby.Tests
{
    static internal class FluentExtensions
    {
        public static AuctionAssertions Should(this Auction testSubject)
        {
            return new AuctionAssertions(testSubject);
        }
    }
}