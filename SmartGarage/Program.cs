using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using SmartGarage.Repositories;
using SmartGarage.Services;
using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using SmartGarage.Data;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using SmartGarage.Helpers;
using SmartGarage.ViewModels;
using System.Threading;
using System;
using Microsoft.AspNetCore.Http;
using SmartGarage.Models;

namespace ForumManagmentSystem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-GB");

            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IVehicleRepository, VehicleRepository>();
            builder.Services.AddScoped<IServiceRepository, ServiceRepository>();

            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IVehicleService, VehicleService>();
            builder.Services.AddScoped<IServiceService, ServiceService>();

            builder.Services.AddScoped<Authenticator>();

            builder.Services.AddHttpContextAccessor();

            builder.Services.AddScoped<IModelMapper, ModelMapper>();
            builder.Services.AddAutoMapper(typeof(Program));
            AutoMapper.IConfigurationProvider cfg = new MapperConfiguration(cfg => { cfg.AddProfile<MapperProfiles>(); });
            builder.Services.AddSingleton(cfg);

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Your API", Version = "v1" });
                options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Description = "Standard Authorization header using the Bearer scheme (\"bearer {token}\")",
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });
            });

            builder.Services.Configure<RequestLocalizationOptions>(options =>
            {
                options.DefaultRequestCulture = new RequestCulture("en-US");
                options.SupportedCultures = new List<CultureInfo> { new CultureInfo("en-US") };
                options.SupportedUICultures = new List<CultureInfo> { new CultureInfo("en-US") };
            });

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value)),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

            builder.Services.AddDbContext<SGDbContext>(options =>
            {
                string connectionString = @"Server=localhost\SQLEXPRESS;Database=SmartGarage;Trusted_Connection=True;";
                options.UseSqlServer(connectionString);
                options.EnableSensitiveDataLogging();
                options.EnableDetailedErrors();
            });

            builder.Services.AddControllersWithViews();
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromHours(24);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            var app = builder.Build();

            var supportedCultures = new[]
            {
                new CultureInfo("en-GB"),
            };

            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("en-GB"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            });

            app.UseSession();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Your API V1");
            });

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Account}/{action=Login}/{id?}"); 
            });
            app.Run();
        }
    }
}
