using System;
using System.Collections.Generic;

namespace RoomOccupancy.Common.Extentions
{
    public static class ExceptionExtensions
    {
        public static IEnumerable<string> WithFailures(this Exception exception)
        {
            var failures = new List<string>();
            // Get messages from inner exceptions
            var currEx = exception;
            do
            {
                failures.Add(currEx.Message);

                currEx = currEx.InnerException;

            } while (currEx != null);

            return failures;
        }
    }
}