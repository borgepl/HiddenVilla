using Business.Config;
using Business.Contracts;
using Business.Repository;
using DataAccess.Data;
using Microsoft.EntityFrameworkCore;

namespace HiddenVilla_Server.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddMyAppServices(this IServiceCollection services, IConfiguration config)

        {

            services.AddDbContext<ApplicationDbContext>(opt =>
            {
                opt.UseSqlServer(config.GetConnectionString("DefaultConnection"));
            });

            services.AddAutoMapper(typeof(MappingProfile));
            services.AddScoped<IHotelRoomRepository, HotelRoomRepository>();

            return services;
        }
    }
}
