using System;

namespace eBaby
{
    public class StoppedClock : Clock
    {
        public override DateTimeOffset Now()
        {
            return DateTimeOffset.Parse("2022-02-02");
        }
    }
}