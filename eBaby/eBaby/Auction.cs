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

            if (HighestBidder is null)
            {
                _postOffice.SendEMail(this.Seller.UserEmail,
                    EmailMessages.AuctionClosedWithoutBids(Itemdescr));
            }
            else
            {
                _postOffice.SendEMail(this.Seller.UserEmail,
                    EmailMessages.AuctionClosedWithBid(Itemdescr));
            }
            
        }

        public void UsePostOffice(PostOffice postOffice)
        {
            _postOffice = postOffice;
        }
    }
}
