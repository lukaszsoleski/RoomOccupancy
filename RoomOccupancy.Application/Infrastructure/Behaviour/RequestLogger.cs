using MediatR.Pipeline;
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

        public RequestLogger(IDateTime dateTime)
        {
            this.dateTime = dateTime;
        }
        public async Task Process(T request, CancellationToken cancellationToken)
        {
            var log = $"{dateTime.Now} {typeof(T).Name} {request.ToString()}"; 


            Console.WriteLine(log);

            await Task.CompletedTask;
        }
    }
}