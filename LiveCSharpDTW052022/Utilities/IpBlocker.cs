using System;
using System.Collections.Generic;

namespace Mercadona.Utilities
{
    using System;
    using System.Collections.Generic;

    public static class IpBlocker
    {
        private static List<FailedLoginAttempt> failedLoginAttempts = new List<FailedLoginAttempt>();
        private static int maxFailedAttempts = 5; // Nombre maximal de tentatives infructueuses autorisées
        private static int blockDurationMinutes = 1; // Durée de blocage en minutes

        public static void ResetFailedAttempts(string ipAddress)
        {
            var failedAttempt = failedLoginAttempts.Find(attempt => attempt.IpAddress == ipAddress);
            if (failedAttempt != null)
            {
                failedAttempt.FailedAttempts = 0; // Réinitialiser le compteur de tentatives infructueuses
                failedAttempt.IsBlocked = false; // Débloquer l'adresse IP
            }
        }

        public static bool IsIpBlocked(string ipAddress)
        {
            var failedAttempt = failedLoginAttempts.Find(attempt => attempt.IpAddress == ipAddress);

            if (failedAttempt != null && failedAttempt.IsBlocked)
            {
                var currentTime = DateTime.Now;
                var blockDuration = TimeSpan.FromMinutes(blockDurationMinutes);

                if (currentTime - failedAttempt.LastFailedAttemptTime < blockDuration)
                {
                    return true; // Adresse IP bloquée
                }
                else
                {
                    failedAttempt.IsBlocked = false; // Débloquer l'adresse IP car la durée de blocage est écoulée
                }
            }

            return false; // Adresse IP non bloquée
        }

        public static void AddFailedAttempt(string ipAddress)
        {
            var failedAttempt = failedLoginAttempts.Find(attempt => attempt.IpAddress == ipAddress);

            if (failedAttempt != null)
            {
                failedAttempt.FailedAttempts++;
                failedAttempt.LastFailedAttemptTime = DateTime.Now;

                if (failedAttempt.FailedAttempts >= maxFailedAttempts)
                {
                    failedAttempt.IsBlocked = true; // Bloquer l'adresse IP
                }
            }
            else
            {
                failedLoginAttempts.Add(new FailedLoginAttempt
                {
                    IpAddress = ipAddress,
                    FailedAttempts = 1,
                    LastFailedAttemptTime = DateTime.Now,
                    IsBlocked = false
                });
            }
        }
    }

}
