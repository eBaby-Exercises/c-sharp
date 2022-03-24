using System;

namespace eBaby
{
    public record Auction(User Seller, string Itemdescr, decimal Startprice, DateTimeOffset Starttime, DateTimeOffset Endtime)
    {
        public AuctionStatus Status { get; private set; }

        public void OnStart()
        {
            Status = AuctionStatus.Running;
        }
    }
}