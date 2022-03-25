using System;
using eBabyServices;

namespace eBaby
{
    public class SellNotifier
    {
        public SellNotifier(PostOffice postOffice)
        {
            PostOffice = postOffice;
        }

        public PostOffice PostOffice { get; private set; }

        public void Notify(User seller, User buyer, string itemName)
        {
            PostOffice.SendEMail(seller.UserEmail,
                EmailMessages.AuctionClosedWithBid(itemName));

            PostOffice.SendEMail(buyer.UserEmail,
                EmailMessages.YouWonTheAuction(itemName));
        }
    }

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

            if (IfAuctionHasNotSold())
            {
                new NoSellNotifier(_postOffice).Notify(this.Seller, Itemdescr);
            }
            else
            {
                new SellNotifier(_postOffice).Notify(this.Seller, this.HighestBidder, Itemdescr);
            }
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
