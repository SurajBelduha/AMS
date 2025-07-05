using AMS.Providers.IProvider;
using AMS.Providers.Mapping;
using AMS.Repository;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AMS.Providers
{
    public static class ServicesConfiguration
    {
        public static void AddProviderServices(this IServiceCollection services, IConfiguration configuration)
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
            services.AddRepositoryService(configuration);
            services.AddTransient<IAppointmentProvider, AppointmentProvider>();

        }
    }
}
