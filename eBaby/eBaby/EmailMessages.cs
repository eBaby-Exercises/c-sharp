namespace eBaby
{
    public static class EmailMessages
    {
        public static string AuctionClosedWithoutBids(string itemname)
        {
            return $"Sorry, your auction for {itemname} did not have any bidders.";
        }

        public static string AuctionClosedWithBid(string itemName)
        {
            return itemName;
        }
    }
}