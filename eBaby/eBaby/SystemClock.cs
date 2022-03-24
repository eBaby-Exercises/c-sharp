using System;

namespace eBaby
{
    public class SystemClock : Clock
    {
        public override DateTimeOffset Now()
        {
            return DateTimeOffset.Now;
        }
    }
}