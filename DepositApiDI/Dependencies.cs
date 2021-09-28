using DepositApi.DAL.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace DepositApiDI
{
    public class Dependencies
    {
        public static void Inject(IServiceCollection services, string connection)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(connection));
        }
    }
}
