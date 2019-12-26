using FluentValidation.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TBP.Filters;
using TBP.Interfaces;

namespace TBP.Configurations
{
    public class MVCConfiguration : IInstaller
    {
        public void Configure(IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient();
            services.AddCors();
            services.AddControllers();
            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(ValidationFilter));
            }).AddFluentValidation(fv => 
            {
                fv.RunDefaultMvcValidationAfterFluentValidationExecutes = false;
            });
        }
    }
}
