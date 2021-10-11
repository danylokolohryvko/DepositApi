using DepositApi.BLL.Mapper;
using DepositApi.DAL.EntityFramework;
using DepositApi.DAL.Models;
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
            services.AddScoped<IRepository<Deposit>, Repository<Deposit>>();
            services.AddScoped<IRepository<DepositCalculation>, Repository<DepositCalculation>>();
            services.AddAutoMapper(typeof(MapperProfile));
        }
    }
}
