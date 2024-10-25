using Microsoft.EntityFrameworkCore;
using WebAppApi.Data;
using WebAppApi.Interfaces;
using WebAppApi.Services;

namespace WebAppApi.Extensions
{
    public static class ApplicationServicesExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddCors(c =>
            {
                c.AddPolicy("AllowAnyOrigin", options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().WithOrigins("http://localhost:4200", "https://localhost:4200"));
            });
            services.AddDbContext<DataContext>(opt =>
            {
                opt.UseSqlite(config.GetConnectionString("DevConnection"));
            });
            services.AddScoped<ITokenService, TokenService>();
            return services;
        }
    }
}
