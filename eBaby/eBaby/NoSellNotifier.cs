using eBabyServices;

namespace eBaby
{
    public class NoSellNotifier : AuctionClosedNotifier
    {
        public readonly PostOffice _postOffice;

        public NoSellNotifier(PostOffice postOffice)
        {
            _postOffice = postOffice;
        }

        public override void Notify(User seller, User buyer, string itemdescr)
        {
            _postOffice.SendEMail(seller.UserEmail,
                EmailMessages.AuctionClosedWithoutBids(itemdescr));
        }
    }
}