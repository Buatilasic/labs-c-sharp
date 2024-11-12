using System;

namespace Pllab
{
    public class ValidationException : Exception
    {
        public static void LogError(Log log, string message)
        {
            log.Error(message);
        }

        public ValidationException(string message) : base(message)
        {
            Log log = new Log();
            LogError(log, message);
        }
    }

}
