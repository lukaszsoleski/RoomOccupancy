using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RoomOccupancy.API.Middleware;
using RoomOccupancy.Application.Campus.Rooms.Commands.CreateRoom;
using RoomOccupancy.Application.Infrastructure;
using RoomOccupancy.Application.Infrastructure.Behaviour;
using RoomOccupancy.Application.Infrastructure.Mapping;
using RoomOccupancy.Application.Interfaces;
using RoomOccupancy.Application.Interfaces.Users;
using RoomOccupancy.Common;
using RoomOccupancy.Domain.Entities.Users;
using RoomOccupancy.Infrastructure.Notifications;
using RoomOccupancy.Infrastructure.Security.Users;
using RoomOccupancy.Infrastructure.SystemClock;
using RoomOccupancy.Persistence;
using RoomOccupancy.Application.Infrastructure.Users;
namespace RoomOccupancy.API
{
    public class Startup
    {
        private const string SecretKey = "iNivDmHLpUA223sqsfhqGbMRdRj1PVkH"; // todo: get this from somewhere secure
        private readonly SymmetricSecurityKey _signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecretKey));
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

            ConfigureJwtSecurity(services);

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
            services.AddIdentity<AppUser, IdentityRole>(options =>
             {
                 // TODO: add more options
                 options.Password.RequiredLength = 4;
             }).AddDefaultTokenProviders();
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

            services.AddTransient<IAuthenticationService, AuthAuthenticationService>();
            services.AddTransient<IJwtFactory, JwtFactory>();
            services.AddTransient<IRegistrationService, RegistrationService>();
            services.AddTransient<IUserService, UserService>();
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

        /// <summary>
        /// Configure JWT authorization and authentication services.
        /// </summary>
        /// <param name="services"></param>
        private void ConfigureJwtSecurity(IServiceCollection services)
        {
            #region JwtIssuerOptions class config
            // jwt wire up
            // Get options from app settings
            var jwtAppSettingOptions = Configuration.GetSection("JwtIssuerOptions");

            // Configure JwtIssuerOptions. This class will be injected via DI Container with the following settings assigned to properties:
            services.Configure<JwtIssuerOptions>(options =>
            {
                options.Issuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
                options.Audience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)];
                options.SigningCredentials = new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256);
            }); // More about configuration and options pattern: https://docs.microsoft.com/en-us/aspnet/core/fundamentals/configuration/options?view=aspnetcore-2.1

            #endregion

            #region Set JWT Token validation options parameters

            SetTokenValidationParameters(out TokenValidationParameters tokenValidationParameters, jwtAppSettingOptions);
            #endregion

            #region Add JWT authentication to the request pipeline
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(configureOptions =>
            {
                configureOptions.ClaimsIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
                configureOptions.TokenValidationParameters = tokenValidationParameters;
                configureOptions.SaveToken = true;

            });
            #endregion

            #region Add authorization policy
            // api user claim policy
            services.AddAuthorization(options =>
            {
                options.AddPolicy("ApiUser", policy => policy.RequireClaim(Constants.Strings.JwtClaimIdentifiers.Rol, Constants.Strings.JwtClaims.ApiAccess));
            });
            #endregion
        }
        /// <summary>
        /// Specifies the validation parameters to dictate how tokens received from users gets validated
        /// </summary>
        /// <param name="parameters"></param>
        /// <param name="jwtAppSettingOptions"></param>
        private void SetTokenValidationParameters(out TokenValidationParameters parameters, IConfigurationSection jwtAppSettingOptions)
        {
            parameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)],

                ValidateAudience = true,
                ValidAudience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)],

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = _signingKey,

                RequireExpirationTime = false,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };
        }
    }
}
