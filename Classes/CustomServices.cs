using afi_demo.Classes.Entities;
using afi_demo.Services;
using afi_demo.Services.Interfaces;
using afi_demo.Services.Repositories;
using afi_demo.Services.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace afi_demo.Classes;

public static class AddServices
{
    public static void AddCustomServices(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddTransient<IDemoService, DemoService>();
        services.AddTransient<IDemoRepository, DemoRepository>();

        services.AddHttpContextAccessor();


        services.AddDbContext<AppDbContext>((sp, opt) =>
        {
            opt.UseSqlServer(configuration["ConnectionStrings:SqlServer"], o => o.UseRelationalNulls());
        });

    }
}

