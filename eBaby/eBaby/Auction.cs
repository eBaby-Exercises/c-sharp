using System;

namespace eBaby
{
    public record Auction(User Seller, string Itemdescr, decimal Startprice, DateTimeOffset Starttime,
        DateTimeOffset Endtime)
    {
        public AuctionStatus Status { get; private set; }
        public decimal HighestBid { get; set; }
        public User HighestBidder { get; set; }

        public void OnStart()
        {
            Status = AuctionStatus.Running;
        }

        public void OnClose()
        {
            Status = AuctionStatus.Closed;
        }
    }
}
