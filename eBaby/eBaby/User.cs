using System;
using System.Data.Common;

namespace eBaby
{
    public record User(string firstName, string lastName, string userEmail, string userName, string password);
}
