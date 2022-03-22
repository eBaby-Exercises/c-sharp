using System;
using eBaby;

static internal class Arbitrary
{
    public static User User()
    {
        string randomUserName = Guid.NewGuid().ToString();
        return new User(string.Empty, string.Empty, string.Empty, randomUserName, string.Empty);
    }
}