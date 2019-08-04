using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RoomOccupancy.Application.Campus.Rooms.Commands.CreateRoom;
using RoomOccupancy.Application.Infrastructure;
using RoomOccupancy.Application.Infrastructure.Behaviour;
using RoomOccupancy.Application.Infrastructure.Mapping;
using RoomOccupancy.Application.Interfaces;
using RoomOccupancy.Common;
using RoomOccupancy.Infrastructure.Notifications;
using RoomOccupancy.Infrastructure.SystemClock;
using RoomOccupancy.Persistence;

namespace RoomOccupancy.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            RegisterFrameworkServices(services);
            //Add AutoMapper
            // Configure AutoMapper: 
         
            // Add MediatR
            services.AddMediatR(typeof(CreateRoomCommand).GetTypeInfo().Assembly);
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPerformanceBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehaviour<,>));

            #region Add DbContext
            // Configure DB Context. Add EbookShop context to dependency injection container and set database provider and also connection string. 
            services.AddDbContext<IReservationDbContext,ReservationDbContext>(options => 
                options.UseSqlServer(connectionString: Configuration.GetConnectionString("RoomOccupancyDatabase")));
            #endregion

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CreateRoomCommandValidator>());
        }

        private void RegisterFrameworkServices(IServiceCollection services)
        {
            services.AddTransient<IDateTime, MachineDateTime>();
            services.AddTransient<INotificationService, NotificationService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
