using eBabyServices;

namespace eBaby
{
    public class SellNotifier : AuctionClosedNotifier
    {
        public SellNotifier(PostOffice postOffice)
        {
            PostOffice = postOffice;
        }

        public PostOffice PostOffice { get; private set; }

        public override void Notify(User seller, User buyer, string itemName)
        {
            PostOffice.SendEMail(seller.UserEmail,
                EmailMessages.AuctionClosedWithBid(itemName));

            PostOffice.SendEMail(buyer.UserEmail,
                EmailMessages.YouWonTheAuction(itemName));
        }
    }
}