using DepositApi.Core.Intrerfaces;
using DepositApi.Core.Models;
using DepositApi.DAL.EntityFramework;
using DepositApi.DAL.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DepositApiDI
{
    public class Dependencies
    {
        public static void Inject(IServiceCollection services, string connection)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(connection));
            services.AddScoped<IRepository<DepositModel>, Repository<DepositModel>>();
            services.AddScoped<IRepository<DepositCalculationModel>, Repository<DepositCalculationModel>>();
        }
    }
}
