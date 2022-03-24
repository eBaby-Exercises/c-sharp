using System;
using eBaby;

static internal class Arbitrary
{
    public static User User()
    {
        string randomUserName = Guid.NewGuid().ToString();
        return UserWithUserName(randomUserName);
    }

    public static User UserWithUserName(string randomUserName)
    {
        return new User(string.Empty,
            string.Empty, string.Empty, randomUserName, "right_password");
    }

    public static User RegisteredUser(out UserRegistry registry)
    {
        User user = Arbitrary.User();
        registry = new UserRegistry();
        registry.Add(user);
        return user;
    }

    public static Auction Auction()
    {
        var user = Arbitrary.UserWithUserName("right_user");
        user.BecomeSeller();
        var clock = new StoppedClock();
        var result = user.CreateAuction("ItemDescr", 23.95m, clock.Now()
            , clock.Now());
        return result;
    }
}