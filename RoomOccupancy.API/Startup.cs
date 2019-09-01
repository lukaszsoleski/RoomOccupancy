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
using RoomOccupancy.API.Middleware;
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
            RegisterInfrastructureServices(services);

            // Add AutoMapper
            services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);

            AddMediatR(services);

            ConfigureDbContext(services);
            // add CORS before MVC
            services.AddCors();
            AddMvc(services);
        }

        private static void AddMvc(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                //.AddJsonOptions(opt => opt.SerializerSettings.DateFormatString = "dd-MM-yyyy HH:mm:ss")
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CreateRoomCommandValidator>());
        }

        private void ConfigureDbContext(IServiceCollection services)
        {
            // Configure DB Context. Add EbookShop context to dependency injection container and set database provider and also connection string. 
            services.AddDbContext<IReservationDbContext, ReservationDbContext>(options =>
                options.UseSqlServer(connectionString: Configuration.GetConnectionString("RoomOccupancyDatabase")));
        }

        private static void AddMediatR(IServiceCollection services)
        {
            // Add MediatR
            services.AddMediatR(typeof(CreateRoomCommand).GetTypeInfo().Assembly);
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPerformanceBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehaviour<,>));
        }

        private void RegisterInfrastructureServices(IServiceCollection services)
        {

            // Add Infrastructure
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

            AddMiddleware(app);
            app.UseCors(options =>
            {
                options.AllowAnyOrigin();
                options.AllowAnyMethod();
                options.AllowAnyHeader();
                
            });
            app.UseMvc();
        }

        private static void AddMiddleware(IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionHandlingMiddleware>();
        }
    }
}
