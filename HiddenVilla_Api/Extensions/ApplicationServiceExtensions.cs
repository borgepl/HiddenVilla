using Business.Config;
using Business.Contracts;
using Business.Repository;
using DataAccess.Data;
using HiddenVilla_Api.Config;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Stripe;

namespace HiddenVilla_Api.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddMyAppServices(this IServiceCollection services, IConfiguration config)

        {

            services.AddDbContext<ApplicationDbContext>(opt =>
            {
                opt.UseSqlServer(config.GetConnectionString("DefaultConnection"));
            });


            var appSettingsSection = config.GetSection("APISettings");
            services.Configure<APISettings>(appSettingsSection);

            services.AddAutoMapper(typeof(MappingProfile));
           
            services.AddScoped<IHotelRoomRepository, HotelRoomRepository>();
            services.AddScoped<IHotelImagesRepository, HotelImagesRepository>();
            services.AddScoped<IAmenityRepository, AmenityRepository>();
            services.AddScoped<IRoomOrderDetailsRepository, RoomOrderDetailsRepository>();

            services.AddCors(opt =>
            {
                opt.AddPolicy("CorsPolicyAllowAll", policy =>
                {
                    policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
                    // .WithOrigins("http://localhost:4200")
                    // .WithOrigins("");
                });
            });

            services.AddRouting(opt => opt.LowercaseUrls=true);

            return services;
        }

        public static IApplicationBuilder UseStripe(this IApplicationBuilder app, IConfiguration config )
        {
            StripeConfiguration.ApiKey = config.GetSection("Stripe")["ApiKey"];

            return app;
        }

    }
}
