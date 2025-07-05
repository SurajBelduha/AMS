using AMS.Repository.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace AMS.Repository
{
    public static class ServicesConfiguration
    {
        public static void AddRepositoryService(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<TestContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        }
    }
}
