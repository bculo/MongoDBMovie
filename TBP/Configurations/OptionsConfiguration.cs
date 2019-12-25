using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TBP.Interfaces;
using TBP.Options;

namespace TBP.Configurations
{
    public class OptionsConfiguration : IInstaller
    {
        public void Configure(IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<SecurityOptions>(configuration.GetSection(nameof(SecurityOptions)));
            services.Configure<MovieDatabaseOptions>(configuration.GetSection(nameof(MovieDatabaseOptions)));
        }
    }
}
