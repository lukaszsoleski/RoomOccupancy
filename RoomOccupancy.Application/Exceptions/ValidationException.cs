using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation.Results;

namespace RoomOccupancy.Application.Exceptions
{
#pragma warning disable RCS1194 // Implement exception constructors.
    public class ValidationException : Exception
#pragma warning restore RCS1194 // Implement exception constructors.
    {
        public ValidationException()
            : base("One or more validation failures have occurred.")
        {
            Failures = new Dictionary<string, string[]>();
        }
        public ValidationException(string failure): base(failure)
        {

        }
        public ValidationException(List<ValidationFailure> failures)
            : this()
        {
            var propertyNames = failures
                .Select(e => e.PropertyName)
                .Distinct();

            foreach (var propertyName in propertyNames)
            {
                var propertyFailures = failures
                    .Where(e => e.PropertyName == propertyName)
                    .Select(e => e.ErrorMessage)
                    .ToArray();

                Failures.Add(propertyName, propertyFailures);
            }
        }

        public IDictionary<string, string[]> Failures { get; }
    }
}
