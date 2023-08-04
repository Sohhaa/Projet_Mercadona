using System;
namespace Mercadona.Utilities
{
    public class FailedLoginAttempt
    {
        public string IpAddress { get; set; }
        public int FailedAttempts { get; set; }
        public DateTime LastFailedAttemptTime { get; set; }
        public bool IsBlocked { get; set; }
    }

}
