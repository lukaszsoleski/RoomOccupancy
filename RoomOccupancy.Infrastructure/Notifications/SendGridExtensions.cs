using Microsoft.Extensions.DependencyInjection;
using RoomOccupancy.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace RoomOccupancy.Infrastructure.Notifications
{
    public static class SendGridExtensions
    {
        /// <summary>
        /// Injects the <see cref="SendGridEmailSender"/> into the services to handle the <see cref="INotificationService"/>
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddSendGridEmailSender(this IServiceCollection services)
        {
            services.AddTransient<INotificationService, SendGridEmailSender>();
            return services;
        }
    }
}
