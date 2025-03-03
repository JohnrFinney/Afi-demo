using afi_demo.Services;
using afi_demo.Services.Interfaces;
using System;

namespace afi_demo.Classes;

public static class AddServices
{
    public static void AddCustomServices(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddTransient<IDemoService, DemoService>();

        services.AddHttpContextAccessor();


        //services.AddDbContext<AppDbContext>((sp, opt) =>
        //{
        //    opt.UseSqlServer(config["ConnectionStrings:SqlServer"], o => o.UseRelationalNulls());
        //});

    }
}

