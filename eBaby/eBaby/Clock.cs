using System;

namespace eBaby
{
    public abstract class Clock
    {
        public abstract DateTimeOffset Now();
    }
}