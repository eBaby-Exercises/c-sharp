namespace eBaby
{
    public abstract class AuctionClosedNotifier
    {
        public abstract void Notify(User seller, User buyer, string itemdescr);
    }
}