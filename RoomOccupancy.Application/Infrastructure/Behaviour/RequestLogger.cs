using MediatR.Pipeline;
using Microsoft.Extensions.Logging;
using RoomOccupancy.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RoomOccupancy.Application.Infrastructure
{
    /// <summary>
    /// Logger executed for each request.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class RequestLogger<T> : IRequestPreProcessor<T>
    {
        private readonly IDateTime dateTime;
        private readonly ILogger<T> logger;

        public RequestLogger(IDateTime dateTime, ILogger<T> logger)
        {
            this.dateTime = dateTime;
            this.logger = logger;
        }
        public async Task Process(T request, CancellationToken cancellationToken)
        {
            var log = $"{dateTime.Now} {typeof(T).Name} {request.ToString()}";

            logger.LogInformation(log);

            await Task.CompletedTask;
        }
    }
}