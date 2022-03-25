using System;
using eBabyServices;

namespace eBaby
{
    public record Auction(User Seller, string Itemdescr, decimal Startprice, DateTimeOffset Starttime,
        DateTimeOffset Endtime)
    {
        private PostOffice _postOffice= PostOffice.GetInstance();
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

            var notifier = CreateClosedNotifier();
            notifier.Notify(this.Seller, this.HighestBidder, Itemdescr);
        }

        private AuctionClosedNotifier CreateClosedNotifier()
        {
            AuctionClosedNotifier notifier;
            if (IfAuctionHasNotSold())
            {
                notifier = new NoSellNotifier(_postOffice);
            }
            else
            {
                notifier = new SellNotifier(_postOffice);
            }

            return notifier;
        }

        private bool IfAuctionHasNotSold()
        {
            return HighestBidder is null;
        }

        public void UsePostOffice(PostOffice postOffice)
        {
            _postOffice = postOffice;
        }
    }
}
