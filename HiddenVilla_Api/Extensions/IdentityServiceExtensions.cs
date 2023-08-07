using DataAccess.Data;
using DataAccess.Data.Identity;
using Microsoft.AspNetCore.Identity;

namespace HiddenVilla_Api.Extensions
{
    public static class IdentityServiceExtensions
    {
        public static IServiceCollection AddMyIdentityServices(this IServiceCollection services, IConfiguration config)
        {

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options => {
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 5;
                // options.Password.RequireDigit = true;
                options.Lockout.MaxFailedAccessAttempts = 3;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(2);
                // options.SignIn.RequireConfirmedEmail = true;
                // options.SignIn.RequireConfirmedAccount = true;
            });

            return services;
        }

    }
}
