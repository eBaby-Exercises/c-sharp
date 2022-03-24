using System;
using System.Data.Common;

namespace eBaby
{
    public record User(string FirstName, string LastName, string UserEmail, string UserName, string Password)
    {
        public bool IsLoggedIn { get; private set; }
        public bool IsSeller { get; private set; }

        public void TryToLogIn(string password)
        {
            if (password == Password)
            {
                IsLoggedIn = true;
            }
            else
            {
                throw new BadCredentialsException();
            }
             
        }

        public void LogOut()
        {
            IsLoggedIn = false;
        }

        public void BecomeSeller()
        {
            IsSeller = true;
        }

        public Auction CreateAuction(string itemdescr, decimal startPrice, DateTimeOffset startTime,
            DateTimeOffset endTime)
        {
            if (!IsSeller)
            {
                throw new NotAuthorizedException();
            }
            return new Auction(this, itemdescr, startPrice, startTime, endTime);
        }

        public void Bid(Auction runningAuction, decimal amount)
        {
            runningAuction.HighestBid = amount;
            runningAuction.HighestBidder = this;
        }
    }
}
