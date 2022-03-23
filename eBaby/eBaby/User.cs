﻿using System;
using System.Data.Common;

namespace eBaby
{
    public record User(string FirstName, string LastName, string UserEmail, string UserName, string Password)
    {
        public bool IsLoggedIn { get; private set; }

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
    }
}
