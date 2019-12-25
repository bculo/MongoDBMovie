using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TBP.Interfaces
{
    public interface IInstaller
    {
        void Configure(IServiceCollection services, IConfiguration configuration);
    }
}
