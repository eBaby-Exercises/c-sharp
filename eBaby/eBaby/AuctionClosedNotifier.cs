namespace eBaby
{
    public abstract class AuctionClosedNotifier
    {
        public abstract void Notify(User seller, string itemdescr);
    }
}